using System.Collections.Generic;

public class DialogScript
{
    public static Dictionary<DialogName, Dictionary<Language, (DialogSpeaker, string[])[]>> DialogData = new()
    {
        {DialogName.Tutorial, new ()
        {
            {
                Language.Kr, new []{
                    (DialogSpeaker.Mother, new string[]{ "엄마" }),
                    (DialogSpeaker.Tsukino, new string[]{ "츠키노" }),
                    (DialogSpeaker.Mother, new string[]{ "엄마" }),
                    (DialogSpeaker.Tsukino, new string[]{ "츠키노" }),
                    (DialogSpeaker.Mother, new string[]{ "엄마" }),
                    (DialogSpeaker.Tsukino, new string[]{ "츠키노" })
                }
            },
            {
                Language.En, new []{
                    (DialogSpeaker.Mother, new string[]{ "En" }),
                    (DialogSpeaker.Tsukino, new string[]{ "En" }),
                    (DialogSpeaker.Mother, new string[]{ "En" }),
                    (DialogSpeaker.Tsukino, new string[]{ "En" }),
                    (DialogSpeaker.Mother, new string[]{ "En" }),
                    (DialogSpeaker.Tsukino, new string[]{ "En" })
                }
            }
        }},
        {DialogName.Act1, new ()
        {
            {
                Language.Kr, new []{
                    (DialogSpeaker.Tsukino, new string[]{
                        "츠키노",
                        "츠키노",
                        "츠키노",
                        "한글"
                    })
                }
            },
            {
                Language.En, new []{
                    (DialogSpeaker.Tsukino, new string[]{
                        "En",
                        "En",
                        "En",
                        "En"
                    })
                }
            }
        }},
    };
}
