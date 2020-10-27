using AmongUs.Api;
using AmongUs.Loader;
using UnityEngine;

namespace PauseCountdownMod
{
	public class StopCountdownMod : Mod {

		private static bool _blocked = false;

		public StopCountdownMod() : base("StopCountdown", "Countdown Stopper", "1.0.0") {
			Description = "Allows starting games to be cancelled with right click. For that friend who clicks start too early.";
			Authors = new[] {"George Kazanjian"};
		}

		public override void Load() {

			GameLobby.UpdateEvent += manager => {
				//Min players for debugging only
				// manager.MinPlayers = 0;
				
				if (_blocked) manager.CountDownTimer = 10;
				
				if (Input.GetMouseButtonDown(1)) {
					_blocked = !_blocked;
					manager.ResetStartState();
				}

				//Only add our text if the timer is within the default range
				if(manager.CountDownTimer <= 5 && manager.CountDownTimer > 0) manager.GameStartText += "\nRight Click To Stop.";
			};

			//Unblock the timer whenever the start button is pressed
			GameLobby.TryStartEvent += manager => {
				_blocked = false;
			};
		}
	}
}
