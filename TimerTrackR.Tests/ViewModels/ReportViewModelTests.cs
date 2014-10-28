using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TimeTrackR.Core.Timer;
using TimeTrackR.ViewModels;

namespace TimerTrackR.Tests.ViewModels
{
    [TestFixture]
    public class ReportViewModelTests
    {
        private ReportViewModel CreateViewModelWithSingleHistoryItem(DateTime itemStartDate, DateTime itemEndDate, DateTime filterStartDate, DateTime filterEndDate)
        {
            var historyItems = new List<TimerHistoryItem>
                               {
                                   new TimerHistoryItem
                                   {
                                       Start = itemStartDate,
                                       End = itemEndDate
                                   }
                               };

            return new ReportViewModel(historyItems)
                   {
                       Filter_StartDateTime = filterStartDate,
                       Filter_EndDateTime = filterEndDate
                   };
        }

        [Test]
        public void FilteredHistoryItems_OutOfRange_Earlier_ItemNotIncluded()
        {
            var viewModel = CreateViewModelWithSingleHistoryItem(new DateTime(2014, 10, 28, 19, 15, 00),
                                                                 new DateTime(2014, 10, 28, 19, 20, 00),
                                                                 new DateTime(2014, 10, 28, 19, 25, 00),
                                                                 new DateTime(2014, 10, 28, 19, 30, 00));

            Assert.That(viewModel.FilteredHistoryItems, Is.Empty);
        }

        [Test]
        public void FilteredHistoryItems_OutOfRange_Later_ItemNotIncluded()
        {
            var viewModel = CreateViewModelWithSingleHistoryItem(new DateTime(2014, 10, 28, 19, 15, 00),
                                                                 new DateTime(2014, 10, 28, 19, 20, 00),
                                                                 new DateTime(2014, 10, 28, 19, 05, 00),
                                                                 new DateTime(2014, 10, 28, 19, 10, 00));

            Assert.That(viewModel.FilteredHistoryItems, Is.Empty);
        }

        [Test]
        public void FilteredHistoryItems_FullyInRange_ItemIsIncluded()
        {
            var viewModel = CreateViewModelWithSingleHistoryItem(new DateTime(2014, 10, 28, 19, 15, 00),
                                                                 new DateTime(2014, 10, 28, 19, 20, 00),
                                                                 new DateTime(2014, 10, 28, 19, 14, 00),
                                                                 new DateTime(2014, 10, 28, 19, 21, 00));

            Assert.That(viewModel.FilteredHistoryItems.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FilteredHistoryItems_StartOnlyInRange_ItemIsIncluded()
        {
            var viewModel = CreateViewModelWithSingleHistoryItem(new DateTime(2014, 10, 28, 19, 15, 00),
                                                                 new DateTime(2014, 10, 28, 19, 20, 00),
                                                                 new DateTime(2014, 10, 28, 19, 14, 00),
                                                                 new DateTime(2014, 10, 28, 19, 16, 00));

            Assert.That(viewModel.FilteredHistoryItems.Count(), Is.EqualTo(1));
        }

        [Test]
        public void FilteredHistoryItems_EndOnlyInRange_ItemIsIncluded()
        {
            var viewModel = CreateViewModelWithSingleHistoryItem(new DateTime(2014, 10, 28, 19, 15, 00),
                                                                 new DateTime(2014, 10, 28, 19, 20, 00),
                                                                 new DateTime(2014, 10, 28, 19, 19, 00),
                                                                 new DateTime(2014, 10, 28, 19, 21, 00));

            Assert.That(viewModel.FilteredHistoryItems.Count(), Is.EqualTo(1));
        }
    }
}