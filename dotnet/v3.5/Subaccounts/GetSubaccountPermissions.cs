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
  /// This example displays all of the available subaccount permissions.
  ///
  /// To get a subaccount ID, run GetSubaccounts.cs.
  /// </summary>
  class GetSubaccountPermissions : SampleBase {
    /// <summary>
    /// Returns a description about the code example.
    /// </summary>
    public override string Description {
      get {
        return "This example displays all of the available user role permissions for the" +
            " specified subaccount.\n\nTo get a subaccount ID, run GetSubaccounts.cs.\n";
      }
    }

    /// <summary>
    /// Main method, to run this code example as a standalone application.
    /// </summary>
    /// <param name="args">The command line arguments.</param>
    public static void Main(string[] args) {
      SampleBase codeExample = new GetSubaccountPermissions();
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
      long subaccountId = long.Parse(_T("INSERT_SUBACCOUNT_ID_HERE"));

      // Limit the fields returned.
      String fields = "userRolePermissions(id,name)";

      // Retrieve the subaccount.
      Subaccount subaccount = service.Subaccounts.Get(profileId, subaccountId).Execute();

      // Retrieve the subaccount permissions.
      UserRolePermissionsResource.ListRequest request = service.UserRolePermissions.List(profileId);
      request.Ids = subaccount.AvailablePermissionIds.Select(p => p.ToString()).ToList<string>();
      request.Fields = fields;

      UserRolePermissionsListResponse permissions = request.Execute();

      // Display the subaccount permissions.
      foreach (UserRolePermission permission in permissions.UserRolePermissions) {
        Console.WriteLine("User role permission with ID {0} and name \"{1}\" was found.",
            permission.Id, permission.Name);
      }
    }
  }
}
