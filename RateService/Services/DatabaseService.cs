using Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using EmailSender;

namespace RateService.Services
{
    public class DatabaseService
    {
        readonly SqlConnection connection;
        private readonly EmailHandling _sender;
        private RateDatabaseContext Context { get; set; }
        private double _lastValue;
        private DateTime _lastDate;
        private readonly List<string> _mails;
        private Tuple<string, string> tuple;

        public DatabaseService()
        {
            Context = new RateDatabaseContext();
            _sender = new EmailHandling();
            connection = (SqlConnection)Context.Database.Connection;
            _mails = new List<string>();
            _lastValue = -1.0;
            _lastDate = DateTime.Now;
            tuple = null;
        }

        private async Task<int> InsertChecking(double value)
        {
            
            try
            {
                connection.Open();

                SqlParameter Value = new SqlParameter 
                { 
                    ParameterName = "Value",
                    Value = value,
                    Direction = ParameterDirection.Input,
                    SqlDbType = SqlDbType.Money 
                };

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $@"[dbo].[InsertCheckingProcedure]",
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.Add(Value);

                var response = await command.ExecuteNonQueryAsync();

                return response;

            }
            catch (Exception)
            {

                return -1;

            }
            finally
            {
                connection.Close();
            }

        }

        private async Task<double> FindChecking()
        {

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $@"[dbo].[SelectCheckingProcedure]",
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    _lastValue = double.Parse(reader["yenValue"].ToString());
                    _lastDate = DateTime.Parse(reader["checkDate"].ToString());
                }
                else
                {
                    _lastValue = -1.0;
                }

                return 1;

            }
            catch (Exception)
            {

                return -1.0;

            }
            finally
            {
                connection.Close();
            }

        }

        private async Task<int> GetMails()
        {

            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand
                {
                    Connection = connection,
                    CommandText = $@"[dbo].[SelectEmailProcedure]",
                    CommandType = CommandType.StoredProcedure
                };

                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    _mails.Add(reader["yenValue"].ToString());
                }

                return 1;

            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }

        }

        public async Task DoWork(double value)
        {
            var _insertResponse = await InsertChecking(value);

            if (_insertResponse != -1)
            {
                var _checkingResponse = await FindChecking();

                if (_checkingResponse != -1 && _lastValue != -1)
                {
                    var _mailResponse = await GetMails();

                    if (_mailResponse != -1 && _mails != null)
                    {
                        if (value > _lastValue) 
                        {
                            tuple = new Tuple<string, string>("¡Subió el valor!", $"En la última fecha: {_lastDate} el yen tenía un valor de {_lastValue} por cada Euro.\nEn estos momentos, vale {value} por cada Euro.") ;
                        }
                        else if (value == _lastValue)
                        {
                            tuple = new Tuple<string, string>("¡El valor se mantuvo!", $"En la última fecha: {_lastDate} el yen tenía un valor de {_lastValue} por cada Euro.\nEn estos momentos, vale {value} por cada Euro.");
                        }
                        else if (value < _lastValue)
                        {
                            tuple = new Tuple<string, string>("¡Bajó el valor!", $"En la última fecha: {_lastDate} el yen tenía un valor de {_lastValue} por cada Euro.\nEn estos momentos, vale {value} por cada Euro.");
                        }
                        else
                        {
                            tuple = null;
                        }

                        if (tuple != null)
                        {
                            foreach (var mail in _mails)
                            {
                                _sender.SendEmail(mail, tuple.Item1, tuple.Item2);
                            }
                        }
                    }
                }
            }
            else
            {

            }

        } 

    }
}
