using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageGenerator : MonoBehaviour {

	public GameObject ImageGray;
	public GameObject ImageBlack;
	public GameObject Player1;
	public GameObject Player2;
	public GameObject Box;
	public GameObject Item;
	public GameObject Bomb;
	public GameObject BombAdd;

	public GameObject ButtonJudge;

	private const int ROW = 9;
	private const int COL = 9;

	private int[,] field = new int[ROW, COL];
	public bool[,] fieldBomb = new bool[ROW, COL];

	private const int PLAYER1 = 1;
	private const int PLAYER2 = 2;
	private const int BOX = 3;
	private const int ITEM = 4;
	private const int MAXBOX = 50;

	private float[,] bombPosition = new float[301, 2];	//ワールド座標
	private int[,] bombPosition2 = new int[301, 2];		//field座標
	private int bombCount = 0;
	public float bombRange11 = 0.5f;
	public int bombRange12 = 1;
	public int bombItemCount1 = 0;
	public int bombItemCount2 = 0;
	public float bombRange21 = 0.5f;
	public int bombRange22 = 1;
	public float[,] bombAtPosi = new float[81, 2];//爆風の座標を保存
	public bool flagBombAt;
	public int AtCount = 0;


	// Use this for initialization
	void Start () {
		init ();
	}

	// Update is called once per frame
	void Update () {

	}

	void judge() {
		bool flag1 = false;
		bool flag2 = false;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER1) {
					flag1 = true;
				} else if (field [j, i] == PLAYER2) {
					flag2 = true;
				}
			}
		}
		if (!flag1 && !flag2) {
			/* 引き分け */
			ButtonJudge.SetActive (true);
			ButtonJudge.GetComponentInChildren<Text> ().text = "引き分け";
		} else if (!flag1) {
			/* Player2の勝ち */
			ButtonJudge.SetActive (true);
			ButtonJudge.GetComponentInChildren<Text> ().text = "Player2（赤）\nの勝ち！！";
		} else if (!flag2) {
			/* Player1の勝ち */
			ButtonJudge.SetActive (true);
			ButtonJudge.GetComponentInChildren<Text> ().text = "Player1（青）\nの勝ち！！";
		}
	}

	void init() {
		bombCount = 0;
		bombRange11 = 0.5f;
		bombRange12 = 1;
		bombItemCount1 = 0;
		bombRange21 = 0.5f;
		bombRange22 = 1;
		bombItemCount1 = 0;
		flagBombAt = false;

		/* field初期化 */
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				field [j, i] = 0;
				fieldBomb [j, i] = false;
			}
		}

		/* player配置 */
		field [0, 0] = PLAYER2;
		field [8, 8] = PLAYER1;

		/* ブロック配置 */
		int m = 0, n = 0;
		for (float j = -2.0f; j < 2.1f; j+=0.5f) {
			n = 0;
			for (float i = -2.0f; i < 2.1f; i+=0.5f) {
				if (m % 2 != 0 && n % 2 != 0) {
					GameObject go = (GameObject)Instantiate (ImageBlack);
					go.transform.position = new Vector3 (j, i, 0);
				} else {
					GameObject go2 = (GameObject)Instantiate (ImageGray);
					go2.transform.position = new Vector3 (j, i, 0);
				}
				n += 1;
			}
			m += 1;
		}

		/* BOX,ITEM配置 */
		m = 0; n = 0;
		for (int k = 0, t = 0, count = 0; k < MAXBOX && count < 1000;) {
			m = 0;
			int randX = Random.Range (0, ROW);
			int randY = Random.Range (0, COL);
			int randiX = Random.Range(1,ROW-1);
			int randiY = Random.Range(1,COL-1);
			for (float j = 2.0f; j > -2.1f; j -= 0.5f, m++) {
				n = 0;
				for (float i = -2.0f; i < 2.1f; i += 0.5f, n++) {
					if (field[m, n] != BOX && (m % 2 == 0 || n % 2 == 0) && (m == randX && n == randY) &&
						!(m == 8 && n == 8) && !(m == 8 && n == 7) && !(m == 7 && n == 8) && !(m == 0 && n == 0) && !(m == 0 && n == 1) && !(m == 1 && n == 0)) {
						field [m, n] = BOX;
						GameObject go = (GameObject)Instantiate (Box);
						go.transform.position = new Vector3 (i, j, 0);
						k++;
					}
					if (t < 6 && field[m,n] == BOX && field [m, n] != ITEM && (m % 2 == 0 || m % 2 == 0) && (m == randiX && n == randiY)) {
						field [m, n] = ITEM;
						GameObject goi = (GameObject)Instantiate (Item);
						goi.transform.position = new Vector3 (i, j, 0);
						t++;
					}
				}// i
			}// j
			count++;
		}// k

	}//init

	/* 爆弾配置 */
	public void PushBomb1() {
		flagBombAt = false;//この関数には関係ない
		if (bombCount < 300) {
			for (int j = 0; j < ROW; j++) {
				for (int i = 0; i < COL; i++) {
					if (field [j, i] == PLAYER1) {
						fieldBomb [j, i] = true;
						StartCoroutine(BombFlag (j, i));
					}
				}
			}
			/* 生成 */
			GameObject bomb1 = (GameObject)Instantiate (Bomb);
			bomb1.transform.position = Player1.transform.position;
			/* playerの位置取得、爆風が不可領域を避けるため（座標の奇数を判定したい） */
			for (int j = 0; j < ROW; j++) {
				for (int i = 0; i < COL; i++) {
					if (field [j, i] == PLAYER1) {
						bombPosition2[bombCount, 0] = j;
						bombPosition2[bombCount, 1] = i;
					}
				}
			}
			/* 爆弾の座標取得、ワールド座標 */
			bombPosition [bombCount, 0] = bomb1.transform.position.x;
			bombPosition [bombCount, 1] = bomb1.transform.position.y;
			bombCount++;
			StartCoroutine(BombAddFunc (PLAYER1));
		}
	}

	public void PushBomb2() {
		flagBombAt = false;//この関数には関係ない
		if (bombCount < 300) {
			for (int j = 0; j < ROW; j++) {
				for (int i = 0; i < COL; i++) {
					if (field [j, i] == PLAYER2) {
						fieldBomb [j, i] = true;
						StartCoroutine(BombFlag (j, i));
					}
				}
			}
			/* 生成 */
			GameObject bomb2 = (GameObject)Instantiate (Bomb);
			bomb2.transform.position = Player2.transform.position;
			/* playerの位置取得、爆風が不可領域を避けるため（座標の奇数を判定したい） */
			for (int j = 0; j < ROW; j++) {
				for (int i = 0; i < COL; i++) {
					if (field [j, i] == PLAYER2) {
						bombPosition2[bombCount, 0] = j;
						bombPosition2[bombCount, 1] = i;
					}
				}
			}
			/* 爆弾の座標取得 */
			bombPosition [bombCount, 0] = bomb2.transform.position.x;
			bombPosition [bombCount, 1] = bomb2.transform.position.y;
			bombCount++;
			StartCoroutine(BombAddFunc (PLAYER2));
		}
	}

	IEnumerator BombFlag(int x, int y) {
		yield return new WaitForSeconds (3.0f);
		fieldBomb [x, y] = false;
	}

	/* 爆風 */
	IEnumerator BombAddFunc (int player) {
		yield return new WaitForSeconds (2.5f);
		AtCount = 0;
		/* 爆風生成 */
		float bombRange = 0;
		int m = 0, n = 0;
		if (player == PLAYER1) {
			bombRange = bombRange11;
			m = -bombRange12;
			n = bombRange12;
		} else if (player == PLAYER2) {
			bombRange = bombRange21;
			m = -bombRange22;
			n = bombRange22;
		}
		for (float j = -bombRange; j <= (bombRange+0.1f); j += 0.5f, m++) {
			if ((bombPosition [0, 0] + j > -2.1f && bombPosition [0, 0] + j < 2.1f) &&
				(bombPosition2[0, 1]+m >= 0 && bombPosition2[0, 1]+m < 9) && !(bombPosition2[0, 0] % 2 != 0 && bombPosition2[0, 1] + m % 2 != 0)) {
				GameObject bombadd = (GameObject)Instantiate (BombAdd);
				bombadd.transform.position = new Vector3 (bombPosition [0, 0] + j, bombPosition [0, 1], 0.0f);
				field [bombPosition2 [0, 0], bombPosition2 [0, 1] + m] = 0;
				/* 爆風の座標保存、BoxDeleterのため */
				bombAtPosi [AtCount, 0] = bombPosition [0, 0] + j;
				bombAtPosi [AtCount, 1] = bombPosition [0, 1];
				AtCount++;
			}
		}
		for (float i = -bombRange; i <= (bombRange+0.1f); i += 0.5f, n--) {
			if ((bombPosition [0, 1] + i > -2.1f && bombPosition [0, 1] + i < 2.1f) &&
				(bombPosition2[0, 0]+n >= 0 && bombPosition2[0, 0]+n < 9) &&  !(bombPosition2[0, 0] + n % 2 != 0 && bombPosition2[0, 1] % 2 != 0)) {
				GameObject bombadd = (GameObject)Instantiate (BombAdd);
				bombadd.transform.position = new Vector3 (bombPosition [0, 0], bombPosition [0, 1] + i, 0.0f);
				field [bombPosition2 [0, 0] + n, bombPosition2 [0, 1]] = 0;
				/* 爆風の座標保存、BoxDeleterのため */
				bombAtPosi [AtCount, 0] = bombPosition [0, 0];
				bombAtPosi [AtCount, 1] = bombPosition [0, 1] + i;
				AtCount++;
			}
		}
		/* 一つずつずらす */
		for (int j = 0; j < bombCount-1; j++) {
			bombPosition [j, 0] = bombPosition [j + 1, 0];
			bombPosition [j, 1] = bombPosition [j + 1, 1];
			bombPosition2 [j, 0] = bombPosition2 [j + 1, 0];
			bombPosition2 [j, 1] = bombPosition2 [j + 1, 1];
		}
		bombCount--;

		/* 勝敗判定 */
		judge ();
	}

	/* Player1 ボタン　ここから */
	public void PushPlayer1_Up () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER1) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pj > 0 && (pj-1 % 2 == 0 || pi % 2 == 0) && !fieldBomb[pj-1,pi] && field[pj-1,pi] != PLAYER2 && field[pj-1,pi] != BOX && field[pj-1,pi] != ITEM) {
			field [pj - 1, pi] = field [pj, pi];
			field [pj, pi] = 0;
			Player1.transform.position = new Vector3 (Player1.transform.position.x, Player1.transform.position.y + 0.5f, 0.0f);
		}
	}

	public void PushPlayer1_Down () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER1) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pj < ROW-1 && (pj+1 % 2 == 0 || pi % 2 == 0) && !fieldBomb[pj+1,pi] && field[pj+1,pi] != PLAYER2 && field[pj+1,pi] != BOX && field[pj+1,pi] != ITEM) {
			field [pj + 1, pi] = field [pj, pi];
			field [pj, pi] = 0;
			Player1.transform.position = new Vector3 (Player1.transform.position.x, Player1.transform.position.y - 0.5f, 0.0f);
		}
	}

	public void PushPlayer1_Right () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER1) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pi < COL-1 && (pj % 2 == 0 || pi+1 % 2 == 0) && !fieldBomb[pj,pi+1] && field[pj,pi+1] != PLAYER2 && field[pj,pi+1] != BOX && field[pj,pi+1] != ITEM) {
			field [pj, pi + 1] = field [pj, pi];
			field [pj, pi] = 0;
			Player1.transform.position = new Vector3 (Player1.transform.position.x + 0.5f, Player1.transform.position.y, 0.0f);
		}
	}

	public void PushPlayer1_Left () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER1) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pi > 0 && (pj % 2 == 0 || pi-1 % 2 == 0) && !fieldBomb[pj,pi-1] && field[pj,pi-1] != PLAYER2 && field[pj,pi-1] != BOX && field[pj,pi-1] != ITEM) {
			field [pj, pi - 1] = field [pj, pi];
			field [pj, pi] = 0;
			Player1.transform.position = new Vector3 (Player1.transform.position.x - 0.5f, Player1.transform.position.y, 0.0f);
		}
	}
	/* Player1 ボタン　ここまで */

	/* Player2 ボタン　ここから */
	public void PushPlayer2_Up () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER2) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pj < ROW-1 && (pj+1 % 2 == 0 || pi % 2 == 0) && !fieldBomb[pj+1,pi] && field[pj+1,pi] != PLAYER1 && field[pj+1,pi] != BOX && field[pj+1,pi] != ITEM) {
			field [pj + 1, pi] = field [pj, pi];
			field [pj, pi] = 0;
			Player2.transform.position = new Vector3 (Player2.transform.position.x, Player2.transform.position.y - 0.5f, 0.0f);
		}
	}

	public void PushPlayer2_Down () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER2) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pj > 0 && (pj-1 % 2 == 0 || pi % 2 == 0) && !fieldBomb[pj-1,pi] && field[pj-1,pi] != PLAYER1 && field[pj-1,pi] != BOX && field[pj-1,pi] != ITEM) {
			field [pj - 1, pi] = field [pj, pi];
			field [pj, pi] = 0;
			Player2.transform.position = new Vector3 (Player2.transform.position.x, Player2.transform.position.y + 0.5f, 0.0f);
		}
	}

	public void PushPlayer2_Right () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER2) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pi > 0 && (pj % 2 == 0 || pi-1 % 2 == 0) && !fieldBomb[pj,pi-1] && field[pj,pi-1] != PLAYER1 && field[pj,pi-1] != BOX && field[pj,pi-1] != ITEM) {
			field [pj, pi - 1] = field [pj, pi];
			field [pj, pi] = 0;
			Player2.transform.position = new Vector3 (Player2.transform.position.x - 0.5f, Player2.transform.position.y, 0.0f);
		}
	}

	public void PushPlayer2_Left () {
		int pj = 0, pi = 0;
		for (int j = 0; j < ROW; j++) {
			for (int i = 0; i < COL; i++) {
				if (field [j, i] == PLAYER2) {
					pj = j;
					pi = i;
				}
			}
		}
		if(pi < COL-1 && (pj % 2 == 0 || pi+1 % 2 == 0) && !fieldBomb[pj,pi+1] && field[pj,pi+1] != PLAYER1 && field[pj,pi+1] != BOX && field[pj,pi+1] != ITEM) {
			field [pj, pi + 1] = field [pj, pi];
			field [pj, pi] = 0;
			Player2.transform.position = new Vector3 (Player2.transform.position.x + 0.5f, Player2.transform.position.y, 0.0f);
		}
	}
	/* Player1 ボタン　ここまで */
}
