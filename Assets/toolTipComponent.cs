using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolTipComponent : MonoBehaviour
{
    [SerializeField] GameObject[] ToolTips;
    public bool isTT = false;
    [SerializeField] bool oneShotTT = false,closebutton = false;
    public int toolNum, weaponNum=0;
    int[] numCount;
    public bool active = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ToolTips.Length; i++)
        {
            ToolTips[i].GetComponent<Transform>().rotation = Quaternion.Euler(90, 0, 0);
        }
        numCount = new int[ToolTips.Length];
        for (int i = 0; i < numCount.Length; i++)
        {
            if (i != 4)
                numCount[i] = i;
            else if (i == 4)
                numCount[i] = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        toolTipOpen();
        toolTipClose();
    }

    public void toolTipOpen()
    {
        if(isTT == true && numCount[toolNum] <= toolNum)
        {
            GameSet.gms.tools = true;
            ToolTips[toolNum].transform.Rotate(new Vector3(-200, 0, 0) * Time.deltaTime);
            if (ToolTips[toolNum].transform.rotation.x <= 0)
            {
                ToolTips[toolNum].transform.rotation = Quaternion.Euler(0, 0, 0);
                GameSet.gms.isMouse = true;
                GameSet.gms.MouseOnOff();
                numCount[toolNum]++;
                isTT = false;
            }
        }
    }

    public void CloseButton()
    {
        closebutton = true;
        GameSet.gms.tools = false;
    }

    void toolTipClose()
    {
        if (closebutton == true)
        {
            ToolTips[toolNum].transform.Rotate(new Vector3(200, 0, 0) * Time.deltaTime);
            if (ToolTips[toolNum].transform.rotation.x >= 0.701f)
            {
                ToolTips[toolNum].transform.rotation = Quaternion.Euler(90, 0, 0);
                closebutton = false;
            }
            GameSet.gms.isMouse = false;
            GameSet.gms.MouseOnOff();
        }

    }

    public void Next()
    {
        ToolTips[toolNum].transform.GetChild(0).gameObject.SetActive(false);
        ToolTips[toolNum].transform.GetChild(1).gameObject.SetActive(true);
    }
    
}
