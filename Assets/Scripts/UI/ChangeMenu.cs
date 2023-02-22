using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class ChangeMenu : MonoBehaviourPunCallbacks
{
    /*[Header("skin")]
        [SerializeField] public UnityEngine.U2D.Animation.SpriteResolver mySpriteResolver;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Barbas;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Cuerpo;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Escudos;
        [SerializeField] public UnityEngine.U2D.Animation.SpriteLibrary Pelos;
        [SerializeField] public Renderer ShaderPelos;
        [SerializeField] public Renderer ShaderBarbas;
        [SerializeField] public Color color;

    private void Start() {

    }

    void Update() {
        
    }

    public void changeSkin(string Arma,string Barbas,string Cuerpo,string Escudos,string Pelos, float R, float G, float B)
        {
                Pv.RPC("RPC_changeSkin", RpcTarget.AllBuffered,  Arma, Barbas, Cuerpo, Escudos, Pelos, R, G, B);//nombre funcion, a quien se lo paso, valor
        }
        [PunRPC]
        public void RPC_changeSkin(string Arma,string Barbas,string Cuerpo,string Escudos,string Pelos, float R, float G, float B)
        {
                Color color = new Color(
                        R,
                        G,
                        B
                );
                this.ShaderPelos.material.SetColor("_Color1", color);
                this.ShaderBarbas.material.SetColor("_Color1", color);
                this.Barbas.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Barbas);
                this.Cuerpo.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Cuerpo);
                this.Escudos.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Escudos);
                this.Pelos.spriteLibraryAsset = Resources.Load<UnityEngine.U2D.Animation.SpriteLibraryAsset>(Pelos);
                this.arma.Armas = Resources.Load<Armas>(Arma);
                mySpriteResolver.ResolveSpriteToSpriteRenderer();

        }*/
}
