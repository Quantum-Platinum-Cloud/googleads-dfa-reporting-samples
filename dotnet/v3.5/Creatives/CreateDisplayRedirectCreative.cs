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
using Google.Apis.Dfareporting.v3_5;
using Google.Apis.Dfareporting.v3_5.Data;

namespace DfaReporting.Samples {
  /// <summary>
  /// This example creates a display redirect creative associated with a given
  /// advertiser. To get a size ID, run GetSize.cs.
  /// </summary>
  class CreateDisplayRedirectCreative : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example creates a display redirect creative associated" +
            " with a given advertiser. To get a size ID, run GetSize.cs.\n";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new CreateDisplayRedirectCreative();
      Console.WriteLine(codeExample.Description);
      codeExample.Run(DfaReportingFactory.getInstance());
    }

    /// <summary>
    /// Run the code example.
    /// </summary>
    /// <param name="service">An initialized Dfa Reporting service object
    /// </param>
    public override void Run(DfareportingService service) {
      long advertiserId = long.Parse(_T("INSERT_ADVERTISER_ID_HERE"));
      long sizeId = long.Parse(_T("INSERT_SIZE_ID_HERE"));
      long profileId = long.Parse(_T("INSERT_USER_PROFILE_ID_HERE"));

      string imageUrl = _T("INSERT_IMAGE_URL_HERE");

      Creative creative = new Creative();
      creative.AdvertiserId = advertiserId;
      creative.Name = "Test display redirect creative";
      creative.RedirectUrl = imageUrl;
      creative.Size = new Size() { Id = sizeId };
      creative.Type = "DISPLAY_REDIRECT";

      Creative result = service.Creatives.Insert(creative, profileId).Execute();

      // Display the new creative ID.
      Console.WriteLine("Display redirect creative with ID {0} was created.", result.Id);
    }
  }
}
