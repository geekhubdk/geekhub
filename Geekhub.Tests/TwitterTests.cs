using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geekhub.Modules.Alerts.Support;
using Geekhub.Modules.Meetings.Models;
using Xunit;

namespace Geekhub.Tests
{
    public class TwitterTests
    {
        [Fact]
        public void FormatTest1()
        {
            var m = new Meeting() {
                City = new City() { Name = "Odense" },
                Title = "Mit møde",
                Id = 1
            };

            var a = new TwitterMeetingAlertGenerator();
            var actual = a.GenerateMessage(m);

            Assert.Equal("Mit møde (1. jan) #odense geekhub.dk/m/1", actual);
        }
    }
}
