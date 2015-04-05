﻿using System;
using Newtonsoft.Json;

namespace WodstarMobileApp
{
	public class Workout
	{
		[JsonProperty(PropertyName = "id")]
		public string id { get; set;}

		//The workout name
		[JsonProperty(PropertyName = "workoutName")]
		public String workoutName { get; set;}

		//The type; benchmark, hero, camille, wodstar
		[JsonProperty(PropertyName = "workoutType")]
		public String workoutType { get; set;}

		public WorkoutSegment[] segments;

		//Add the cool down/end workout one time sessions
		//Need to have a type for each movement in the array - maybe make jagged array of types?

		public Workout (String name, String type, params WorkoutSegment[] segments) {
			this.workoutName = name;
			this.workoutType = type;
			this.segments = segments;
		}

		//Default constructor
		public Workout () {

		}
	}
}
