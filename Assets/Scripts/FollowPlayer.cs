﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, -10);
	}
}
