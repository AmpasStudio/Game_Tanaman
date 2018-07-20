using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControllerGame : MonoBehaviour {
	//keterangantext
	public Text detikText;
	public Text menitText;
	public Text jamText;
	public Text hariText;

	public Text pupukText;
	public Text rumputText;
	public Text intensitasCHYText;
	public Text indikatorAirText;
	public Text moodText;

	//

	//time
	float nextSecond = 1.0f;
	float beforeSecond = 0.0f;
	float detik = 0.0f;
	float menit = 0;
	float jam = 0;
	float hari = 0;
	//time

	public int jumlahPupuk = 0;
	float pupuk = 0;
	public float rumput = 100;
	public float intensitasCHY = 0;
	public bool kenaCahaya = false;
	public float indikatorAir = 0;
	float jumlahPengaruh = 0;
	int rumputHari = 0;
	//
	float mood = 0;
	float tinggi = 0;
	public int maxTinggi = 150;
	//

	public GameObject tanaman;
	public List<GameObject> fase;
	// Use this for initialization
	void Start () {
		Instantiate (tanaman, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
		updateText ();
		beforeSecond += Time.deltaTime;
		if (beforeSecond >= nextSecond) {
			beforeSecond = 0.0f;
			detik++;
			perhitunganMood ();
			intensitas ();
			prosesIndikatorAir ();
			tumbuh ();
			animasiTumbuh ();
			if (detik >= 60.0f) {
				menit++;
				detik = 0.0f;
				if (menit >= 60.0f) {
					jam++;
					menit = 0.0f;


					if(jam==12||jam==0){
						//jumlahpupuk = 0;
					}
					if (jam>=24) {
						hari++;
						jam = 0.0f;
						keluarRumput ();
					}
				}
			}
		}
		Debug.Log ("detik = "+ detik +" menit = "+ menit+" jam = "+ jam + " hari = "+ hari);
	}

	void tambahPupuk(){
		if (jumlahPupuk <= 2) {
			pupuk += 50;
			jumlahPupuk += 1;
		} else {
			Debug.Log ("pupuk kebanyakan tidak bisa lagi");
			//script menampilkan text dilayar
		}
	}

	void tambahAir(){
		if (indikatorAir < 100) {
			indikatorAir = 100;
		} else {
			Debug.Log ("air kebanyakan tidak bisa lagi");
			//script menampilkan text dilayar
		}
	}

	void keluarRumput(){
		rumputHari += 1;
		if (rumputHari > 2) {
			rumputHari = 0;
			rumput = 0;
		}
	}

	void cabutRumput(){
		rumput = 100;
	}

	void intensitas(){
		//collision cahaya
		//if(//kenacahaya){
			
		//}else{
			
		//}
		intensitasCHY = 100;
		kenaCahaya = true;
	}

	void prosesIndikatorAir(){
		if (indikatorAir>0) {
			if (kenaCahaya == false) {
				indikatorAir -= 2;
			}else{
				indikatorAir -= 1;
			}
		}
	}

	void perhitunganMood(){
		mood = (rumput + pupuk + intensitasCHY)/3;
	}

	void tumbuh(){
		if (tinggi < maxTinggi) {
			tinggi = tinggi + mood / 100 * 1.5f;
		}
	}

	void animasiTumbuh(){
		float persenTinggi = tinggi / maxTinggi * 100;
		if (persenTinggi >= 99.0f) {
			fase[4].SetActive(true);
			fase[3].SetActive(false);
		}else if(persenTinggi >= 75.0f){
			fase[3].SetActive(true);
			fase[2].SetActive(false);
		}else if(persenTinggi >= 50.0f){
			fase[2].SetActive(true);
			fase[1].SetActive(false);
		}else if(persenTinggi >= 25.0f){
			fase[1].SetActive(true);
			fase[0].SetActive(false);
		}else{
			fase[0].SetActive(true);
		}
	}

	void updateText(){
		detikText.text = detik.ToString ();
		menitText.text = menit.ToString ();
		jamText.text = jam.ToString ();
		hariText.text = hari.ToString ();

		pupukText.text = pupuk.ToString ();
		rumputText.text = rumput.ToString ();
		intensitasCHYText.text = intensitasCHY.ToString ();
		indikatorAirText.text = indikatorAir.ToString ();
		moodText.text = mood.ToString ();
	}
}

