/**
 * Copyright (c) 2017 The Campfire Union Inc - All Rights Reserved.
 *
 * Licensed under the MIT license. See LICENSE file in the project root for
 * full license information.
 *
 * Email:   info@campfireunion.com
 * Website: https://www.campfireunion.com
 */

using UnityEngine;
using System.Collections;
using _Scripts;

namespace VRKeys {

	/// <summary>
	/// Enter key that calls Submit() on the keyboard.
	/// </summary>
	public class EnterKey : Key {
		public GameObject data;


		void Start()
        {
			data = GameObject.Find("Data");
        }

		public override void HandleTriggerEnter (Collider other) {
			//keyboard.Submit ();
			data.GetComponent<HttpManager>().VRLoginAction();
		}

		public override void UpdateLayout (Layout translation) {
			label.text = translation.enterButtonLabel;
		}
	}
}