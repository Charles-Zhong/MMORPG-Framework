﻿using System;
using UnityEngine;
using GFW;

namespace CodeX
{
	public class PreResStateListner : IGameStateListner
	{
        public override void OnStateEnter(GameState pCurState)
        {
            LogMgr.Log("PreResStateListner@OnStateEnter");
            if (AppConst.UpdateMode)
            {
                ResUpdateManager.Instance.CloseGameDataBase(true);
                ResUpdateManager.Instance.OpenGameVersion();
            }
            //GameSystem.Instance.GetManager<GameManager>().OnInitialize();
            BusinessManager.Instance.SendMessage(ModuleDef.LaunchModule, "SendMessageCommand", "UpdateMessage", GameConfig.Instance["EnterGame"]);
            if (AppConst.UpdateMode)
            {
                GameConfig.Instance.WriteUpdateState("finish");
            }
        }

        public override void OnStateUpdate(GameState pCurState, float elapseTime)
        {
        }

		public override void OnStateQuit(GameState pCurState)
		{
		}

		public override void Free()
		{
		}
	}
}
