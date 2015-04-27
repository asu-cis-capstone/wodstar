﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;

namespace WodstarMobileApp.Droid
{
	[Activity (Label = "UserProfileActivity", Theme="@android:style/Theme.Black.NoTitleBar", Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Portrait)]			
	public class UserProfileActivity : Activity
	{
		private TextView userNameTextView;
		private TextView userInfoTextView;
		private ImageView userProfilePicture;
		private TableLayout userDataTable;
		private Button wodDataButton;
		private Button prDataButton;
		private bool wodVisible;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Create your application here
			SetContentView (Resource.Layout.Journal);

			//Get instances of variable layout components
			userNameTextView = FindViewById<TextView> (Resource.Id.userNameTextView);
			userInfoTextView = FindViewById<TextView> (Resource.Id.userInfoTextView);
			userProfilePicture = FindViewById<ImageView> (Resource.Id.profilePictureImageView);
			userDataTable = FindViewById<TableLayout> (Resource.Id.userDataTable);
			wodDataButton = FindViewById<Button> (Resource.Id.wodButton);
			prDataButton = FindViewById<Button> (Resource.Id.prButton);

			wodDataButton.Click += (sender, e) => {
				createWodTable();
			};
			prDataButton.Click += createPrTable;

			userNameTextView.Text = Util.thisUser.firstName + " " + Util.thisUser.lastName;
			userInfoTextView .Text= "Age: " + Util.thisUser.age + "\nGender: " + Util.thisUser.gender;
			//TODO: Dynamically retrieve user picture and set.

			//Initialize views to Wods
			createWodTable();

			var menu = FindViewById<FlyOutContainer> (Resource.Id.FlyOutContainer);
			var hamburgerButton = FindViewById (Resource.Id.hamburgerButton);
			hamburgerButton.Click += (sender, e) => {
				menu.AnimatedOpened = !menu.AnimatedOpened;
			};

			var titleButton = FindViewById (Resource.Id.titleTextView);
			var homeButton = FindViewById (Resource.Id.homeTextView);
			var profileButton = FindViewById (Resource.Id.profileTextView);
			var wodLibraryButton = FindViewById (Resource.Id.wodLibraryTextView);
			var movementLibraryButton = FindViewById (Resource.Id.movementLibraryTextView);
			var logoutButton = FindViewById (Resource.Id.logoutTextView);

			titleButton.Click += goToHomeScreen;
			homeButton.Click += goToHomeScreen;
			profileButton.Click += goToUserProfile;
			wodLibraryButton.Click += goToWodLibrary;
			movementLibraryButton.Click += goToMovementLibrary;
			logoutButton.Click += goToLogin;
		}

		void createWodTable() 
		{
			if (!wodVisible) {
				wodVisible = true;
				//TODO: Sort Wod Dictionaries
				clearTable ();
				foreach(KeyValuePair<string, Workout> benchmarkWod in WorkoutUtil.benchmarkWods) {
					int baseInt=0;
					foreach(UserJournal j in JournalUtil.wodJournalData) {
						if(j.statId==(benchmarkWod.Value.id)) {
							Console.Out.WriteLine ("Journal entry matching benchmark wod");
							if(j.entryType == JournalUtil.amrapType) {
								Console.Out.WriteLine ("Journal entry is amrap");
								if(Int32.Parse(j.statResult)> baseInt) {
									baseInt = Int32.Parse(j.statResult);
								}
							}
						}
					}

					TableRow dataRow = new TableRow (this);
					TextView workoutName = new TextView (this);

					workoutName.Gravity = GravityFlags.Left;
					workoutName.Text = benchmarkWod.Value.workoutName;
					dataRow.AddView (workoutName);

					if(baseInt!=0) {
						TextView workoutPr = new TextView (this);
						workoutPr.Gravity = GravityFlags.Right;
						workoutPr.Text = baseInt.ToString ();
						dataRow.AddView (workoutPr);
					} else {
						//TODO: Create button, add to layout instead to log new 
					}
						
					userDataTable.AddView (dataRow);
				}
			}
			//User WOD data entry successful

		}

		void createPrTable(object sender, EventArgs e) {
			if (wodVisible) {
				wodVisible = false;
				clearTable ();
				//List<List<int>> prData = Util.thisUser.journal.prStats;
			}
		}

		void addDataToTable(String[][] resultInfo) 
		{
			for(int i = 0; i < resultInfo.Count(); i++) {
				TableRow dataRow = new TableRow (this);
				TextView workoutName = new TextView (this);
				TextView workoutPr = new TextView (this);
				workoutName.Text =resultInfo [i] [0];
				workoutPr.Text = resultInfo [i] [1];
				workoutPr.Gravity = GravityFlags.Right;
				workoutName.Gravity = GravityFlags.Left;
				dataRow.AddView (workoutName);
				dataRow.AddView (workoutPr);

				userDataTable.AddView (dataRow);
			}
			Console.WriteLine ("User WOD data entry successful");
		}

		void clearTable() {

		}

		//NAVIGATION METHODS
		void goToUserProfile(object sender, EventArgs e) {
			//Start a new Activity for the UserProfile layout
			if (this.LocalClassName != "wodstarmobileapp.droid.UserProfileActivity") {
				StartActivity (typeof(UserProfileActivity));
			}
		}

		void goToHomeScreen(object sender, EventArgs e) {
			if (this.LocalClassName != "wodstarmobileapp.droid.StartScreenActivity") {
				StartActivity (typeof(StartScreenActivity));
			}
		}

		void goToWodLibrary(object sender, EventArgs e) {
			if (this.LocalClassName != "wodstarmobileapp.droid.WorkoutLibraryActivity") {
				StartActivity (typeof(WorkoutLibraryActivity));
			}
		}

		void goToLogin(object sender, EventArgs e) {
			if (this.LocalClassName != "wodstarmobileapp.droid.MainActivity") {
				StartActivity (typeof(MainActivity));
			}
		}

		void goToMovementLibrary(object sender, EventArgs e) {
			if (this.LocalClassName != "wodstarmobileapp.droid.MovementLibraryActivity") {
				Console.WriteLine ("Local class name = " + this.LocalClassName.ToString ());
				StartActivity (typeof(MovementLibraryActivity));
			}
		}
		//END NAVIGATION METHODS
	}
}

