﻿/*
 * Copyright 2015 Google Inc
 *
 * Licensed under the Apache License, Version 2.0(the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Linq;
using Google.Apis.Dfareporting.v3_5;
using Google.Apis.Dfareporting.v3_5.Data;

namespace DfaReporting.Samples {
  /// <summary>
  /// This example lists all existing campaigns for the specified user profile.
  /// </summary>
  class GetCampaigns : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example lists all existing campaigns for the specified" +
            " user profile.\n";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetCampaigns();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(DfaReportingFactory.getInstance());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="service">An initialized Dfa Reporting service object
    /// </param>
    public override void Run(DfareportingService service) {
      long profileId = long.Parse(_T("INSERT_PROFILE_ID_HERE"));

      // Limit the fields returned.
      String fields = "nextPageToken,campaigns(id,name)";

      CampaignsListResponse campaigns;
      String nextPageToken = null;

      do {
        // Create and execute the campaigns list request.
        CampaignsResource.ListRequest request =  service.Campaigns.List(profileId);
        request.Fields = fields;
        request.PageToken = nextPageToken;
        campaigns = request.Execute();

        foreach (Campaign campaign in campaigns.Campaigns) {
          Console.WriteLine("Campaign with ID {0} and name \"{1}\" was found.",
              campaign.Id, campaign.Name);
        }

        // Update the next page token.
        nextPageToken = campaigns.NextPageToken;
      } while (campaigns.Campaigns.Any() && !String.IsNullOrEmpty(nextPageToken));
    }
  }
}
