﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Geekhub.App.Modules.Meetings.Models;

namespace Geekhub.App.Modules.Meetings.ViewModels
{
    public class JsonViewModel
    {
        public JsonViewModel(IEnumerable<Meeting> meetings)
        {
            Items = meetings.Select(meeting => new JsonMeetingViewModel(meeting)).ToArray();
        }

        public JsonMeetingViewModel[] Items { get; private set; }

        public sealed class JsonMeetingViewModel 
        {
            public JsonMeetingViewModel(Meeting meeting)
            {
                Id = meeting.Id;
                Title = meeting.Title;
                StartsAt = meeting.StartsAt;
                CreatedAt = meeting.CreatedAt;
                Description = meeting.Description;
                Url = meeting.Url;
                Tags = meeting.Tags.Select(x=>x.Name).ToArray();
                Organizers = meeting.Organizers.Select(x => x.Name).ToArray();
                Address = meeting.AddressFormatted;
                City = meeting.City.Name;
                Latitude = meeting.Latitude;
                Longtitude = meeting.Longtitude;
            }

            public int Id { get; set; }
            public string Title { get; set; }
            public DateTime StartsAt { get; set; }
            public DateTime CreatedAt { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
            public string[] Tags { get; set; }
            public string[] Organizers { get; set; }

            public string Address { get; set; }
            public string City { get; set; }
            public double? Latitude { get; set; }
            public double? Longtitude { get; set; }
        }
    }
}