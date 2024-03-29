﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLionsDen.Model.Requests;
using TheLionsDen.Model.Responses;
using TheLionsDen.Model.SearchObjects;

namespace TheLionsDen.WinUI.Services
{
    public class FacilityAPI : CRUDAPIService<FacilityResponse, FacilitySearchObject, FacilityUpsertRequest, FacilityUpsertRequest>
    {
        public FacilityAPI(string resourceName = "facility") : base(resourceName)
        {
        }
    }
}
