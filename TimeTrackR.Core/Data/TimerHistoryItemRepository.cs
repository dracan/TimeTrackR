using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TimeTrackR.Core.Timer;

namespace TimeTrackR.Core.Data
{
    public class TimerHistoryItemRepository
    {
        private readonly IDataContext _dataContext;

        public TimerHistoryItemRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<TimerHistoryItem> ListAll()
        {
            return _dataContext.TimerHistoryItems;
        }

        public IQueryable<TimerHistoryItem> ListAllBetweenDates(DateTime start, DateTime end)
        {
            return _dataContext.TimerHistoryItems.Where(x => x.Start <= end && start < x.End);
        }

        public void UpdateItems(ICollection<TimerHistoryItem> items)
        {
            var dbContext = _dataContext as DataContext;

            if(dbContext != null)
            {
                foreach(var item in items)
                {
                    UpdateItem(item, false);
                }
            }
        }

        public void UpdateItem(TimerHistoryItem item)
        {
            UpdateItem(item, true);
        }

        private void UpdateItem(TimerHistoryItem item, bool saveChanges)
        {
            var dbContext = _dataContext as DbContext;

            if(dbContext != null)
            {
                dbContext.Entry(item).State = item.id == 0 ? EntityState.Added : EntityState.Modified;
            }

            if(saveChanges)
            {
                _dataContext.SaveChanges();
            }
        }
    }
}