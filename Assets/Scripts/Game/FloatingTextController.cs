using QFramework;
using UnityEngine;

namespace PrjectSurvivor
{
    public partial class FloatingTextController : ViewController
    {
        private static FloatingTextController mDefault;
        void Start()
        {
            // Code Here
            FloatingText.Hide();
        }
        public static void Player(Vector3 pos, float textFol, bool critical = false)
        {
            string text = textFol.ToString("0");
            mDefault.FloatingText.InstantiateWithParent(mDefault.transform)
                .Position(pos.x, pos.y)
                .Self(f =>
            {
                var textTrans = f.Find("Text");
                var textComp = textTrans.GetComponent<UnityEngine.UI.Text>();
                textComp.text = text;
                if (critical)
                {
                    textComp.color = Color.red;
                }
                ActionKit.Sequence()//动作序列
                .Lerp(0, 0.5f, 0.5f, (p) =>//缩放 --- 这里lerp是个自循环的，和Mathf的lerp不一样
                {
                    f.PositionY(pos.y + p * 0.5f);
                    textComp.LocalScaleX(Mathf.Clamp01(p * 8));
                    textComp.LocalScaleY(Mathf.Clamp01(p * 8));
                })
                .Delay(0.5f)
                .Lerp(1.0f, 0, 0.3f, (p) =>//透明度
                {
                    textComp.ColorAlpha(p);
                }, () =>
                {
                    f.DestroyGameObjGracefully();
                })
                .Start(textComp);
            }).Show();

        }
        private void Awake()
        {
            mDefault = this;
        }
        private void OnDestroy()
        {
            mDefault = null;
        }
    }
}
