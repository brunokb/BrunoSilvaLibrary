using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrunoSilvaLibrary.Models.Extended
{
    public class Media
    {
        public Media() { }
        public Media(UserDataSet.TableMediaDataTable dataTable)
        {
            ConvertToMediaList(dataTable);
        }
        public string Title { get; set; }
        public string  Budget { get; set; }
        public string Language { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public IEnumerable<Media> ConvertToMediaList(UserDataSet.TableMediaDataTable dataTable)
        {
            return dataTable.AsEnumerable().Select(row => new Media
            {
                Title = (row["Title"]).ToString(),
                Budget = (row["Budget"]).ToString(),
                Language = (row["LanguageName"]).ToString(),
                Director = (row["DirectorName"]).ToString(),
                Genre = (row["GenreName"]).ToString(),
            });
        }

    }
}