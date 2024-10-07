using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPreview : MonoBehaviour
{
    public GameObject cuerpoPj;
    public GameObject peloPj;
    public GameObject barbaPj;
    public GameObject armaPj;
    public GameObject escudoPj;

    public GameObject cuerpoPw;
    public GameObject peloPw;
    public GameObject barbaPw;
    public GameObject armaPw;
    public GameObject escudoPw;

    private void Update()
    {
       ChangeSpritePreview(); // Keep preview updated
    }

    private void ChangeSpritePreview()
    {
        // Transfer sprites from the character GameObjects to the preview
        UpdatePreviewSprite(cuerpoPj, cuerpoPw);
        UpdatePreviewSprite(peloPj, peloPw);
        UpdatePreviewSprite(barbaPj, barbaPw);
        UpdatePreviewSprite(armaPj, armaPw, true);
        UpdatePreviewSprite(escudoPj, escudoPw, true);
    }

    private void UpdatePreviewSprite(GameObject characterObject, GameObject previewObject, bool checkEnabled = false)
    {
        Image previewImage = previewObject.GetComponent<Image>();
        SpriteRenderer characterSprite = characterObject.GetComponent<SpriteRenderer>();

        if (previewImage != null && characterSprite != null)
        {
            if (!checkEnabled || characterSprite.enabled)
            {
                previewImage.sprite = characterSprite.sprite;
                previewImage.enabled = true;
            }
            else
            {
                previewImage.enabled = false;
            }
        }
    }
}