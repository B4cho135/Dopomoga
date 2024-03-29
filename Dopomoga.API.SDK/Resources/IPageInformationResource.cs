﻿using Dopomoga.Data.Entities.PageInformation;
using Dopomoga.Models.Requests.PageInformation;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dopomoga.API.SDK.Resources
{
    public interface IPageInformationResource
    {
        [Get("/api/info/{pageType}")]
        public Task<PageInformationEntity> GetPageInformation(string pageType);

        [Put("/api/info")]
        public Task<ApiResponse<PageInformationEntity>> UpdatePageInfo(CreatePageInfoRequest model);
    }
}
