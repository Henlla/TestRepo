using UnityEngine;

public class ShopInteracable : Interactable
{
    //private bool isShopOpen;

    public override void OnClicked()
    {
        //if (ObjectManage.Instance.EditMode)
        //{
        //    return;
        //}
        Debug.Log("test shop");
        //ShopManager.Instance.OpenShop();
    }
}
