using GalaSoft.MvvmLight.Messaging;
using MonefyAppView.Messages;
using MonefyAppView.Models;
using MonefyAppView.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MonefyAppView.Services.Classes
{
    public class TransactionsManager
    {
        private readonly IMessenger _messenger;
        private readonly INavigationService _navigationService;
        private readonly DataService _dataService;

        public Dictionary<DateTime, List<TransactionsModel>> _transactionsByDate = new Dictionary<DateTime, List<TransactionsModel>>();
        public ObservableCollection<TransactionsModel> _transactions = new ObservableCollection<TransactionsModel>();

        public TransactionsManager(IMessenger messenger, INavigationService navigationService, DataService dataService)
        {
            _messenger = messenger;
            _navigationService = navigationService;
            _dataService = dataService;

            _messenger.Register<DataMessage>(this, message =>
            {
                if (message.Data is ObservableCollection<TransactionsModel> transactions)
                {
                    _transactions = transactions;
                }
            });
        }

        public bool HasTransactionForDate(DateTime date)
        {
            return _transactionsByDate.ContainsKey(date);
        }

        public void AddTransactions(double price, string senderId, DateTime transactionDate)
        {
            if (!_transactionsByDate.ContainsKey(transactionDate))
            {
                _transactionsByDate[transactionDate] = new List<TransactionsModel>(_transactions);
            }

            var transaction = new TransactionsModel
            {
                Price = price,
                Title = senderId,
                Count = _transactionsByDate[transactionDate].Count + 1
            };

            _transactionsByDate[transactionDate].Add(transaction);
            _transactions.Add(transaction);
        }

        public void SaveTransactionsForDate(DateTime date)
        {
            if (!_transactionsByDate.ContainsKey(date))
            {
                _transactionsByDate.Add(date, new List<TransactionsModel>(_transactions));
            }
            else
            {
                _transactionsByDate[date] = new List<TransactionsModel>(_transactions);
            }
        }
        public void LoadTransactionsForDate(DateTime date)
        {
            if (_transactionsByDate.TryGetValue(date, out var transactionsForDate))
            {
                _transactions.Clear();
                foreach (var transaction in transactionsForDate)
                {
                    _transactions.Add(transaction);
                }
            }
            else
            {
                _transactions.Clear();
            }
        }

        public List<TransactionsModel> GetTransactionsForDate(DateTime date)
        {
            if (_transactionsByDate.ContainsKey(date))
            {
                return _transactionsByDate[date];
            }

            return new List<TransactionsModel>();
        }
    }
}

