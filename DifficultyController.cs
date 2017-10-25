using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyController : MonoBehaviour {

	public float fuzzyLoad;
	private float timer;
	private int controlDifficulty;

	public MasterSpawn masterSpawn;

	public EnemyMasterScript enemy01;
	public EnemyMasterScript enemy02;
	public EnemyMasterScript enemy03;
	public EnemyMasterScript enemy05;

	#region Unity
	void Start()
	{
		timer = 0;
	}

	void Update () 
	{
		FuzzyTimer ();
		ControlCurve ();
	}
	#endregion

	void ControlCurve()
	{
		//EASIEST
		if (controlDifficulty >= 0 && controlDifficulty <= 12)
		{			
			//Debug.Log("EASIEST");
			masterSpawn.qtdEnemiesAllowedOnScene = 2;
			masterSpawn.delayAfterAlert = 1.4f;
			enemy01.weight = 80;
			enemy02.weight = 20;
			enemy03.weight = 0;
			enemy05.weight = 0;
		}

		//EASIER
		else if (controlDifficulty == 13)
		{
			//Debug.Log("EASIER");
			masterSpawn.qtdEnemiesAllowedOnScene = 2;
			masterSpawn.delayAfterAlert = 1.2f;
			enemy01.weight = 45;
			enemy02.weight = 30;
			enemy03.weight = 25;
			enemy05.weight = 0;
		}

		//EASY
		else if (controlDifficulty >= 14 && controlDifficulty <= 15)
		{
			//Debug.Log("EASY");
			masterSpawn.qtdEnemiesAllowedOnScene = 3;
			masterSpawn.delayAfterAlert = 1f;
			enemy01.weight = 25;
			enemy02.weight = 35;
			enemy03.weight = 35;
			enemy05.weight = 5;
		}

		//MEDIUM
		else if (controlDifficulty >= 16 && controlDifficulty <= 18)
		{
			//Debug.Log("MEDIUM");
			masterSpawn.qtdEnemiesAllowedOnScene = 5;
			masterSpawn.delayAfterAlert = 0.8f;
			enemy01.weight = 45;
			enemy02.weight = 10;
			enemy03.weight = 45;
			enemy05.weight = 0;
		}

		//HARD
		else if (controlDifficulty  >= 19 && controlDifficulty <= 20)
		{
			//Debug.Log("HARD");
			masterSpawn.qtdEnemiesAllowedOnScene = 6;
			masterSpawn.delayAfterAlert = 0.6f;
			enemy01.weight = 10;
			enemy02.weight = 70;
			enemy03.weight = 10;
			enemy05.weight = 10;
		}

		//HARDER
		else if (controlDifficulty == 21 || controlDifficulty == 22)
		{
			//Debug.Log("HARDER");
			masterSpawn.qtdEnemiesAllowedOnScene = 8;
			masterSpawn.delayAfterAlert = 0.4f;
			enemy01.weight = 20;
			enemy02.weight = 20;
			enemy03.weight = 55;
			enemy05.weight = 5;
		}

		//HARDEST
		else if (controlDifficulty >= 23)
		{
			//Debug.Log("HARDEST");
			masterSpawn.qtdEnemiesAllowedOnScene = 10;
			masterSpawn.delayAfterAlert = 0.2f;
			enemy01.weight = 10;
			enemy02.weight = 40;
			enemy03.weight = 30;
			enemy05.weight = 25;
		}

		else 
		{
			//Debug.Log("EASY");
			masterSpawn.qtdEnemiesAllowedOnScene = 3;
			masterSpawn.delayAfterAlert = 1f;
			enemy01.weight = 25;
			enemy02.weight = 35;
			enemy03.weight = 35;
			enemy05.weight = 5;
		}
	}

	void FuzzyTimer()
	{
		timer -= Time.deltaTime;
		if(timer <= 0)
		{
			MyFuzzyRules ();
			ResetVariables ();
			timer = fuzzyLoad;
		}
	}
		
	void ResetVariables()
	{
		GameController.GlobalVariables.lastTimeStarAverage *= 0;
	}

	#region Fuzzy
	void MyFuzzyRules()
	{
		LinguisticVariable points = new LinguisticVariable ("Points");
		points.MembershipFunctionCollection.Add (new MembershipFunction ("Lowest", 0, 1, 2, 4));
		points.MembershipFunctionCollection.Add (new MembershipFunction ("Lower", 3, 5, 7, 9));
		points.MembershipFunctionCollection.Add (new MembershipFunction ("Low", 6, 8, 10, 12));
		points.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 11, 13, 15, 17));
		points.MembershipFunctionCollection.Add (new MembershipFunction ("High", 16, 18, 20, 22));
		points.MembershipFunctionCollection.Add (new MembershipFunction ("Higher", 21, 23, 25, 27));
		points.MembershipFunctionCollection.Add (new MembershipFunction ("Highest", 26, 28, 30, 1000));

		LinguisticVariable timeMatch = new LinguisticVariable ("Timtch");
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("Lowest", 0, 5, 10, 15));
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("Lower",10, 15, 20, 25));
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("Low", 20, 25, 30, 35));
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 30, 35, 40, 45));
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("High", 40, 45, 50, 55));
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("Higher", 50, 55, 60, 65));
		timeMatch.MembershipFunctionCollection.Add (new MembershipFunction ("Highest", 60, 65, 70, 120));

		LinguisticVariable mTime = new LinguisticVariable ("Time");
		mTime.MembershipFunctionCollection.Add (new MembershipFunction ("Lowest", 0, 20, 40, 60));
		mTime.MembershipFunctionCollection.Add (new MembershipFunction ("Low", 40, 60, 80, 100));
		mTime.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 80, 100, 120, 140));
		mTime.MembershipFunctionCollection.Add (new MembershipFunction ("High", 120, 140, 160, 180));
		mTime.MembershipFunctionCollection.Add (new MembershipFunction ("Higher", 160, 180, 200, 500));

		LinguisticVariable mortality = new LinguisticVariable ("Mortality");
		mortality.MembershipFunctionCollection.Add (new MembershipFunction ("Low", 0, 0, 2, 4));
		mortality.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 2, 3, 5, 6));
		mortality.MembershipFunctionCollection.Add (new MembershipFunction ("High", 5, 7, 8, 50));

		LinguisticVariable scoresMeanValue = new LinguisticVariable ("Average");
		scoresMeanValue.MembershipFunctionCollection.Add (new MembershipFunction("Low", 0, 2, 4, 6));
		scoresMeanValue.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 5, 7, 9, 11));
		scoresMeanValue.MembershipFunctionCollection.Add (new MembershipFunction ("High", 10, 12, 20, 40));

		LinguisticVariable timeToTakeStar = new LinguisticVariable ("Starmer");
		timeToTakeStar.MembershipFunctionCollection.Add (new MembershipFunction("Fast", 0, 0.5, 0.5, 1));
		timeToTakeStar.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 0.5, 1, 1.5, 2));
		timeToTakeStar.MembershipFunctionCollection.Add (new MembershipFunction ("Slow", 1.5, 2, 2.5, 3));

		LinguisticVariable difficulty = new LinguisticVariable ("Difficulty");
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Easiest", 1, 2, 3, 4));
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Easier", 3, 4, 5, 6));
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Easy", 5, 6, 7, 8));
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Medium", 7, 8, 9, 10));
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Hard", 9, 10, 11, 12));
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Harder", 11, 12, 13, 14));
		difficulty.MembershipFunctionCollection.Add (new MembershipFunction ("Hardest", 13, 14, 15, 16));


		FuzzyEngine fuzzyEngine = new FuzzyEngine ();
		fuzzyEngine.LinguisticVariableCollection.Add (points);
		fuzzyEngine.LinguisticVariableCollection.Add (mTime);
		fuzzyEngine.LinguisticVariableCollection.Add (mortality);
		fuzzyEngine.LinguisticVariableCollection.Add (scoresMeanValue);
		fuzzyEngine.LinguisticVariableCollection.Add (difficulty);
		fuzzyEngine.LinguisticVariableCollection.Add (timeMatch);
		fuzzyEngine.LinguisticVariableCollection.Add (timeToTakeStar);
		fuzzyEngine.Consequent = "Difficulty";


		/*   EASIEST   */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Average IS Low) THEN Difficulty IS Easiest"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Average IS Medium) THEN Difficulty IS Easiest"));
	

		/*   EASIER   */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Points IS Lower) THEN Difficulty IS Easier"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Medium) AND (Average IS Low) THEN Difficulty IS Easier"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Medium) AND (Average IS Medium) THEN Difficulty IS Easier"));


		/*   EASY   */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Time IS Lowest) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Points IS Low) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Timtch IS Low) AND (Points IS Lowest) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Timtch IS Low) AND (Points IS Lower) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Low) AND (Average IS Low) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Low) AND (Average IS Medium) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Medium) AND (Average IS High) THEN Difficulty IS Easy"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Points IS Low) THEN Difficulty IS Easy"));


		/*   MEDIUM   */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Points IS Medium) THEN Difficulty IS Medium"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Low) AND (Time IS Higher) THEN Difficulty IS Medium"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Low) AND (Time IS Higher) THEN Difficulty IS Medium"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Low) AND (Average IS High) THEN Difficulty IS Medium"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS Low) AND (Average IS Medium) THEN Difficulty IS Medium"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Points IS Medium) THEN Difficulty IS Medium"));

		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Timtch IS High) AND (Points IS Low) THEN Difficulty IS Medium"));



		/*   HARD   */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Points IS High) THEN Difficulty IS Hard"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Starmer IS Slow) AND (Points IS Higher) THEN Difficulty IS Hard"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Starmer IS Medium) AND (Points IS Higher) THEN Difficulty IS Hard"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Starmer IS Slow) AND (Points IS Highest) THEN Difficulty IS Hard"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Points IS High) THEN Difficulty IS Hard"));


		/*   HARDER  */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Points IS Higher) THEN Difficulty IS Harder"));
		//fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Starmer IS Slow) AND (Points IS Highest) THEN Difficulty IS Harder"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Starmer IS Medium) AND (Points IS Highest) THEN Difficulty IS Harder"));
		//fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Timtch IS Higher) AND (Points IS High) THEN Difficulty IS Harder"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Points IS Higher) THEN Difficulty IS Harder"));


		/*   HARDEST   */
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Points IS Highest) THEN Difficulty IS Hardest"));
		fuzzyEngine.FuzzyRuleCollection.Add (new FuzzyRule ("IF (Mortality IS High) AND (Points IS Highest) THEN Difficulty IS Hardest"));


		points.InputValue = GameController.GlobalVariables.points;
		mTime.InputValue = GameController.GlobalVariables.gameTime;
		mortality.InputValue = GameController.GlobalVariables.playerDeaths;
		scoresMeanValue.InputValue = GameController.GlobalVariables.lastScoresAverage;
		timeMatch.InputValue = GameController.GlobalVariables.matchTime;
		timeToTakeStar.InputValue = GameController.GlobalVariables.lastTimeStarAverage;


		//Debug.Log ("Defuzzy: " + fuzzyEngine.Defuzzify ());

		double variable = fuzzyEngine.Defuzzify ();
		controlDifficulty = Mathf.RoundToInt ((float)variable);
	}
	#endregion
}
