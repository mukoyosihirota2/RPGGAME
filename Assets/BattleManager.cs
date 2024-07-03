using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class BattleManager : MonoBehaviour
{
    public int maxhp=100;
    public int Power = 10;
    public int hp = 100;
    public int EnemyHp = 30;
    public int Damage = 10;
    public int maxsp = 3;
    public int sp=3;
    public int attack;
    public int Reflect=3;
    public bool PlayerTurn;
    public bool GameClear;
    public bool GameOver;
    public bool Stop;
    public int HealingItem = 5;
    public int HealingPower = 50;
    public GameObject ItemCancel;
    public GameObject ItemOK;
    public GameObject AttackButton;
    public GameObject SkillButton;
    public GameObject ItemButton;
    public GameObject TurnEndButton;
    public Text GameText;
    public GameObject GameOverText;
    public GameObject GameClearText;
    // Start is called before the first frame update
    void Update()
    {
        if (hp <= 0)
        {
            GameOver=true;
        }
        if (EnemyHp <= 0)
        {
            GameClear = true;
        }
        if (GameOver != true&&GameClear!=true)
        {

            if ( Stop == true)
            {
                AttackButton.SetActive(false);
                SkillButton.SetActive(false);
                ItemButton.SetActive(false);
            }
            else if (PlayerTurn == true)
            {
                AttackButton.SetActive(true);
                SkillButton.SetActive(true);
                ItemButton.SetActive(true);
            }
            else
            {
                AttackButton.SetActive(false);
                SkillButton.SetActive(false);
                ItemButton.SetActive(false);
                EnemyTurn();
            }
        }
        else if(GameOver==true)
        {
            GameOverText.SetActive(true);
          
        }
        else if(GameClear==true)
        {
            GameClearText.SetActive(true);
        }
    }
    
    // Update is called once per frame
    public void AttackCommand()
    {

        Attack();
    }
    public void Attack()
    {
        EnemyHp -= Power;
        GameText.text = "プレイヤーは攻撃した結果 " + Power + "ダメージを与えた";
        TurnEnd();
    }
    public  void Reflection()
    {
        //反射。Sp消費。Spが０なら何も起きない
        if (sp > 0)
        {
            sp--;
            Reflect = 3;
            TurnEnd();
        }
        GameText.text = "プレイヤーはカウンターの構えを取っている";
    }
    public void Item()
    {
        if (HealingItem > 0)
        {
            ItemCancel.SetActive(true);
            ItemOK.SetActive(true);
            GameText.text = "本当に使いますか？";
        }
    }
    public void ItemCanselButton()
    {
        //キャンセル
        ItemCancel.SetActive(false);
        ItemOK.SetActive(false);
    }
    public void ItemOKlButton()
    {
        //使った
        HealingItem--;
        hp += HealingPower;
        if (hp > maxhp)
        {
            hp = maxhp;
        }
        ItemCancel.SetActive(false);
        ItemOK.SetActive(false);
        GameText.text = "プレイヤーは回復アイテムを使い、HPを"+HealingPower+"回復した";
        TurnEnd();
    }
    private void EnemyTurn()
    {
        
        if (Reflect > 0)
        {
            int I = Random.Range(1, 100);
            if (I <= 80)
            {
                EnemyHp -= Damage;
                GameText.text = "殴られたがカウンターに成功した！";
                PlayerTurn = true;
            }
            else
            {
                hp -= Damage+5;
                GameText.text = "カウンターに失敗し" + Damage + "のダメージを受けた";
            }
            Reflect--;
        }
        else { 
            hp -= Damage;
            GameText.text = "殴られたプレイヤーは" + Damage + "のダメージを受けた";
        }
        
        PlayerTurn = true;
    }
    public void TurnEnd()
    {
        PlayerTurn = false;
        TurnEndButton.SetActive(true);
        Stop = true;
        
    }
    public void Next()
    {
        Stop = false;
      
            PlayerTurn = false;
       
        TurnEndButton.SetActive(false);
    }
}
