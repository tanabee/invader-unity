using UnityEngine;
using System.Collections;

/**
 * ライセンスページ全般の管理クラス
 */
public class LicenseViewController : MonoBehaviour {

	public void backToStartScene() {
		Application.LoadLevel ("Start");
	}
}
