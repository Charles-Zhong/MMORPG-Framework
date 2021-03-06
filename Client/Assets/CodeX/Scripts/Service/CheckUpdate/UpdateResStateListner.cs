﻿using System;
using UnityEngine;
using GFW;

namespace CodeX
{
	public class UpdateResStateListner : IGameStateListner
	{
		public override void OnStateEnter(GameState pCurState)
		{
			Debug.Log("enter UpdateResStateListner");
            BusinessManager.Instance.SendMessage(ModuleDef.LaunchModule, "SendMessageCommand", "UpdateMessage", GameConfig.Instance["UpdateDetection"]);
			MonoHelper.StartCoroutine(ResUpdateManager.Instance.CheckVersionFile());
		}

		public override void OnStateUpdate(GameState pCurState, float elapseTime)
		{
			float process = ResUpdateManager.Instance.Update() * 100f;
			process = (float)Math.Ceiling((double)process);
            if ((int)process != 0 && (int)process != 1)
			{
				string hight_res = GameConfig.Instance["HightResUpdate"];
				string download_speed = GameConfig.Instance["DownloadSpeed"];
				string cur_download_size = ResUpdateManager.Instance.GetCurDownloadFileInfo();
				string download_speed_info = ResUpdateManager.Instance.GetCurDownloadSpeedInfo();
				string show_message = string.Concat(new object[]{hight_res,",",cur_download_size,",",download_speed_info,",",process," %"});

                BusinessManager.Instance.SendMessage(ModuleDef.LaunchModule, "SendMessageCommand", "UpdateMessage", show_message);
                if (process <= 100f)
				{
                    BusinessManager.Instance.SendMessage(ModuleDef.LaunchModule, "SendMessageCommand", "UpdateProgress", process);
				}
			}
            if (ResUpdateManager.Instance.IsFinish)
			{
                BusinessManager.Instance.SendMessage(ModuleDef.LaunchModule, "SendMessageCommand", "UpdateMessage", GameConfig.Instance["UpdateSuccess"]);
                BusinessManager.Instance.SendMessage(ModuleDef.LaunchModule, "SendMessageCommand", "UpdateProgress", 100);
                CheckUpdateService.Instance.ToPreResState();
			}
		}

        public override void OnStateQuit(GameState pCurState)
        {
        }

		public override void Free()
		{
		}
	}
}
