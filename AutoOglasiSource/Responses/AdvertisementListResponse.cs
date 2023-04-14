using AutoOglasiSource.Helpers;
using AutoOglasiSource.Model.Advertisement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoOglasiSource.Responses
{
    public class AdvertisementListResponse
    {
        public Pagination<AdvertisementListModel> Pagination { get; set; }
    }
}
