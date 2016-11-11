using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Globalization;
public class Clalander : MonoBehaviour {
	public string Months;

	public List<GameObject> Date_obj;
	public List<GameObject> EmptyButtonList;
	public List<GameObject> EmptyButtonList1;
	public GameObject parent_WeekDayPannel;
	public GameObject parent_Pannel;
	public GameObject date_button;

	public GameObject Calendar_Panel;
	public int no_days;
	public int day;
	public int year;
	public int month;
	public Text Year_text;
	public Text Month_text;
	public Text Date_text;
	public string firstday;
	public int emptybutton;
	public int remender;
	public int remendervalue;
	public int month_incrementer=0;
	public Vector3 lastPosition;
	public Vector3 delta;

	public Vector2 firstPressPos;
	public Vector2 secondPressPos;
	public Vector2 currentSwipe;
	public float screenWidth;
	public float screenHeight;
	public float height_CalendarPanel;
	public float width_CalendarPanel;









	void Start()
	{
//		screenWidth = Screen.width;
//		screenHeight = Screen.height;
//		
//		gridSize ();
		// for the main Calendar 
		RectTransform parent3 = Calendar_Panel.GetComponent<RectTransform> ();



		//the week day panel
		RectTransform parent1 = parent_WeekDayPannel.GetComponent<RectTransform> ();
		GridLayoutGroup grid1 = parent_WeekDayPannel.GetComponent<GridLayoutGroup> ();
		//main calendar panel
		RectTransform parent = parent_Pannel.GetComponent<RectTransform> ();
		GridLayoutGroup grid = parent_Pannel.GetComponent<GridLayoutGroup> ();
		//grid.cellSize = new Vector2 ((parent3.rect.width-14)/7,(parent3.rect.height*(float)(0.12))-10);
		grid.cellSize = new Vector2 ((parent3.rect.width-16)/7,(parent.rect.height/6)-2);

		//print ("This is width of parent" + parent3.rect.width);

		height_CalendarPanel = parent3.rect.height;
		width_CalendarPanel = parent3.rect.width;

		//GridLayoutGroup grid3=Calendar_Panel.GetComponent<>




		//grid1.cellSize = new Vector2 ((parent3.rect.width-14)/7,(parent3.rect.height*(float)(0.11))-8);
		grid1.cellSize = new Vector2 ((parent3.rect.width-16)/7,parent1.rect.height);

		DateTime	firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
		firstday = firstDayOfMonth.ToString ("dddd");
		no_days = System.DateTime.DaysInMonth (System.DateTime.Now.Year, System.DateTime.Now.Month);
		year = System.DateTime.Now.Year;
		month = System.DateTime.Now.Month;

	
		//Year_text.text=System.DateTime.Now.Year.ToString();
		//Month_text.text = System.DateTime.Now.Month.ToString();

		Calender_Instanciate (no_days,month,year);


		}







	void Update () 
	{

		//Mouse_Swipe ();
		Touch_Swipe ();

	}


	public void Calender_Instanciate(int no_days,int month, int year)
	{



		Year_text.text=year.ToString();
		Month_text.text = month.ToString();
			DateTime	firstDayOfMonth = new DateTime(year, month, 1);
	firstday = firstDayOfMonth.ToString ("dddd");

		// for empty button list

	switch(firstday)
	{
	case "Sunday":
		emptybutton = 0;
		break;
	case "Monday":
		{
			emptybutton = 1;
		}
		break;
	case "Tuesday":
		{
			emptybutton = 2;
		}
		break;
	case "Wednesday":
		{
			emptybutton = 3;

		}
		break;
	case "Thursday":
		{
			emptybutton = 4;

		}
		break;

	case "Friday":
		{
			emptybutton = 5;
		}
		break;
	case "Saturday":
		{
			emptybutton = 6;

		}
		break;

	}

		for (int i = 0; i < emptybutton; i++) {
			GameObject egob= Instantiate (date_button)as GameObject;
			EmptyButtonList.Add (egob);
			EmptyButtonList[i].transform.SetParent(parent_Pannel.transform, false);
		}




	//for instanciation of the main date buttons

		for (int i = 0; i < no_days; i++) {
			GameObject gob = Instantiate (date_button) as GameObject;
			Date_obj.Add (gob);
			Date_obj [i].transform.SetParent (parent_Pannel.transform, false);
			Date_obj [i].GetComponentInChildren<Text> ().text = (i + 1).ToString ();


			AddListener (Date_obj [i].GetComponent<Button> (), i);
		}

		//empty button after generation of real dates


		remender = (no_days + emptybutton) % 7;
		remendervalue = 7 - remender;



			if ((remendervalue + no_days + emptybutton) <= 35) {
				remendervalue += 7;
			}
			for (int j = 0; j < remendervalue; j++) {
			GameObject egob = Instantiate (date_button)as GameObject;
				EmptyButtonList1.Add (egob);
				EmptyButtonList1 [j].transform.SetParent (parent_Pannel.transform, false);
								//for the extra line

			}

		}



	 public void AddListener(Button b, int i)
	{
		b.onClick.AddListener(() => Work_onClick(i));
	}





	public void Work_onClick(int i)
	{
		//print ("button click : "+i);
		//print ("Selected Date is " + Date_obj[i].GetComponentInChildren<Text>().text+"-"+System.DateTime.Now.Month+"-"+System.DateTime.Now.Year);

		print ("Selected Date is " + Date_obj[i].GetComponentInChildren<Text>().text+"-"+month+"-"+year);

		if (i== 8) {
			print ("Happy Birthday");
		}
		//print(System.DateTime.MinValue.DayOfWeek)
	}





	public void Increase_year()
	{
		Delete_ExtraButtons ();
		year=year+1;
		month = month + 0;

		no_days=System.DateTime.DaysInMonth(year,month);
		print (no_days);


		Calender_Instanciate (no_days,month,year);
		print (no_days);


	}




	public void Decrease_year()
	{

		Delete_ExtraButtons ();
		year=year-1;
		month = month + 0;

		no_days=System.DateTime.DaysInMonth(year,month);
		print (no_days);


		Calender_Instanciate (no_days,month,year);
		print (no_days);
	}




	public void Increse_Month()
	{
		Delete_ExtraButtons ();

		if (month <=12) {
			month = month +1;
			year = year + 0;
		}
		if (month >12) {
			month = 1;
			year = year + 1;
		}

		no_days = System.DateTime.DaysInMonth (year, month);
		print (no_days);
		Calender_Instanciate (no_days,month,year);
	}





	public void Decrease_Month()
	{
		Delete_ExtraButtons ();

		if (month >= 1) {
			month = month - 1;
			year += 0;
		}
		if (month < 1) {
			month = 12;
			year = year - 1;
		}
		no_days = System.DateTime.DaysInMonth (year, month);
		print (no_days);
		Calender_Instanciate (no_days,month,year);
	}




	public void Check_Button()
	{

		DateTime	firstDayOfMonth = new DateTime(year, month, 1);
		firstday = firstDayOfMonth.ToString ("dddd");
		print (firstday);
	}






	public void Delete_ExtraButtons()
	{
		for (int i = 0; i < EmptyButtonList.Count; i++) {
			Destroy (EmptyButtonList [i]);
		}
		EmptyButtonList.Clear ();
		for (int i = 0; i < Date_obj.Count; i++) {
			Destroy (Date_obj [i]);
		}
		Date_obj.Clear ();

		for (int i = 0; i < EmptyButtonList1.Count; i++) {
			Destroy (EmptyButtonList1 [i]);
		}
		EmptyButtonList1.Clear ();
	}



	// mouse swip function

	public void Mouse_Swipe()
	{
		if(Input.GetMouseButtonDown(0))
		{
			//save began touch 2d point
			firstPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
		}
		if(Input.GetMouseButtonUp(0))
		{
			//save ended touch 2d point
			secondPressPos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);

			//create vector from the two points
			currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

			//normalize the 2d vector
			currentSwipe.Normalize();
		

			//swipe left
			if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				Decrease_Month();
				Debug.Log("left swipe");
			}
			//swipe right
			if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
			{
				Increse_Month();
				Debug.Log("right swipe");
			}
		}

	}

// touch swip function


	public void Touch_Swipe()
	{
		if(Input.touches.Length > 0)
		{
			Touch t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//save began touch 2d point
				firstPressPos = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//save ended touch 2d point
				secondPressPos = new Vector2(t.position.x,t.position.y);

				//create vector from the two points
				currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

				//normalize the 2d vector
				currentSwipe.Normalize();

				//swipe left
				if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					Debug.Log("left swipe");
					Increse_Month ();
				
				}
				//swipe right
				if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
				{
					Debug.Log("right swipe");
					Decrease_Month ();
				}
			}
		}
	}









}


