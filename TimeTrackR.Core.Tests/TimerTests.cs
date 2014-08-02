using System.Threading;
using NUnit.Framework;

namespace TimeTrackR.Core.Tests
{
    [TestFixture]
    public class TimerTests
    {
        [Test]
        public void InitialTimerStateIsStopped()
        {
            var timer = new Timer.Timer();
            Assert.That(timer.State == Timer.Timer.States.Stopped);
        }

        [Test]
        public void TimerStateIsStartedAfterStart()
        {
            var timer = new Timer.Timer();
            timer.Start();
            Assert.That(timer.State == Timer.Timer.States.Started);
        }

        [Test]
        public void TimerStateIsStoppedAfterStop()
        {
            var timer = new Timer.Timer();
            timer.Start();
            timer.Stop();
            Assert.That(timer.State == Timer.Timer.States.Stopped);
        }

        [Test]
        public void FiveSecondSimpleTest()
        {
            var timer = new Timer.Timer();
            timer.Start();
            Thread.Sleep(5000);
            timer.Stop();

            Assert.That(timer.TotalTime.TotalSeconds, Is.InRange(4.9, 5.1));
        }

        [Test]
        public void FiveSecondSimpleTestWithThreeSecondPause()
        {
            var timer = new Timer.Timer();
            timer.Start();
            Thread.Sleep(2500);
            timer.Stop();
            Thread.Sleep(3000);
            timer.Start();
            Thread.Sleep(2500);
            timer.Stop();

            Assert.That(timer.TotalTime.TotalSeconds, Is.InRange(4.9, 5.1));
        }

        [Test]
        public void EnsureResetResetsTotalTime()
        {
            var timer = new Timer.Timer();
            timer.Start();
            Thread.Sleep(500);
            timer.Stop();
            timer.Reset();

            Assert.That(timer.TotalTime.TotalSeconds, Is.EqualTo(0));
        }

        [Test]
        public void EnsureResetResetsState()
        {
            var timer = new Timer.Timer();
            timer.Start();
            timer.Stop();
            timer.Reset();

            Assert.That(timer.State, Is.EqualTo(Timer.Timer.States.Stopped));
        }
    }
}
