using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GoogleSheetManager : MonoBehaviour
{
	const string URL = "https://script.google.com/macros/s/AKfycbwf54HPVAhcNRzVtCh_3jh2AusLts9DPu9MzgqggZ7oqkscEeXfp_IjWuey9qYv4ORRjA/exec";
	public InputField NameInput, reviewTxtInput;
	public Button saveButton;
	public Transform saveRecord;
	public Stopwatch stopWatch;

    private void Awake()
    {
		saveButton.onClick.AddListener(() =>
		{
			if(Register())
            {
				saveRecord.gameObject.SetActive(false);
            }
		});
    }

    private void OnEnable()
    {
		stopWatch.StopTimer();
	}

    bool SetIDPass(string name, string reviewTxt)
	{
		if (name == "" || reviewTxt == "") return false;
		else return true;
	}


	public bool Register()
	{
		string name = NameInput.text;
		float timeScore = stopWatch.m_TotalSeconds;
		string reviewTxt = reviewTxtInput.text;

		if (!SetIDPass(name, reviewTxt))
		{
			print("���̵� �Ǵ� ���渻�� ����ֽ��ϴ�");
			return false;
		}

		WWWForm form = new WWWForm();
		form.AddField("Name", name);
		form.AddField("TimeScore", timeScore.ToString());
		form.AddField("ReviewTxt", reviewTxt);

		StartCoroutine(Post(form));
		return true;
	}


	//public void Login()
	//{
	//	if (!SetIDPass())
	//	{
	//		print("���̵� �Ǵ� ��й�ȣ�� ����ֽ��ϴ�");
	//		return;
	//	}

	//	WWWForm form = new WWWForm();
	//	form.AddField("order", "login");
	//	form.AddField("id", id);
	//	form.AddField("pass", pass);

	//	StartCoroutine(Post(form));
	//}

	IEnumerator Post(WWWForm form)
	{
		using (UnityWebRequest www = UnityWebRequest.Post(URL, form)) // �ݵ�� using�� ����Ѵ�
		{
			yield return www.SendWebRequest();

			if (www.isDone) /*Response(www.downloadHandler.text);*/
            {
				Debug.Log("�������忡 �Է� ��");
            }
			else print("���� ������ �����ϴ�.");
		}
	}


	//void Response(string json)
	//{
	//	if (string.IsNullOrEmpty(json)) return;

	//	GD = JsonUtility.FromJson<GoogleData>(json);

	//	if (GD.result == "ERROR")
	//	{
	//		print(GD.order + "�� ������ �� �����ϴ�. ���� �޽��� : " + GD.msg);
	//		return;
	//	}

	//	print(GD.order + "�� �����߽��ϴ�. �޽��� : " + GD.msg);

	//	if (GD.order == "getValue")
	//	{
	//		ValueInput.text = GD.value;
	//	}
	//}
}