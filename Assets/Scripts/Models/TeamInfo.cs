using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SocketIO;
using System;

	public class TeamInfo : MonoBehaviour
    {
        public string code;
        public int score;
        public string name;
        public string id;

    /**
    public TeamInfo(string code, int score, string name)
    {
        this.code = code;
        this.score = score;
        this.name = name;
    }
    **/

        public override string ToString(){
			return UnityEngine.JsonUtility.ToJson (this, true);
		}


        void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
	}


