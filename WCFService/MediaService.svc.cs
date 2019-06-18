using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BrunoSilvaLibrary.Models.Extended;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MediaService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MediaService.svc or MediaService.svc.cs at the Solution Explorer and start debugging.
    public class MediaService : IMediaService
    {
        public List<Media> MediaReturn(string title, string genre, string director)
        {
            List<Media> md = new List<Media>();
            MediaDataImp mdImp = new MediaDataImp();
            md = mdImp.GetMedias(title, genre, director);
            return md;
        }


    }
}
