using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteSort : MonoBehaviour
{

    public Dictionary<string, Texture> listCreator(List<Texture> sprites)
    {
        Dictionary<string, Texture> spriteStuff = new Dictionary<string, Texture>();
        foreach (Texture t in sprites)
        {
            if (t.name.Equals("001"))
            {
                spriteStuff.Add("Bulbasaur", t);
            }
            else if (t.name.Equals("003"))
            {
                spriteStuff.Add("Venusaur", t);
            }
            else if (t.name.Equals("004"))
            {
                spriteStuff.Add("Charmander", t);
            }
            else if (t.name.Equals("006"))
            {
                spriteStuff.Add("Charizard", t);
            }
            else if (t.name.Equals("007"))
            {
                spriteStuff.Add("Squirtle", t);
            }
            else if (t.name.Equals("009"))
            {
                spriteStuff.Add("Blastoise", t);
            }
            else if (t.name.Equals("037"))
            {
                spriteStuff.Add("Vulpix", t);
            }
            else if (t.name.Equals("059"))
            {
                spriteStuff.Add("Arcanine", t);
            }
            else if (t.name.Equals("062"))
            {
                spriteStuff.Add("Poliwrath", t);
            }
            else if (t.name.Equals("066"))
            {
                spriteStuff.Add("Machop", t);
            }
            else if (t.name.Equals("073"))
            {
                spriteStuff.Add("Tentacruel", t);
            }
            else if (t.name.Equals("101"))
            {
                spriteStuff.Add("Electrode", t);
            }
            else if (t.name.Equals("106"))
            {
                spriteStuff.Add("Hitmonlee", t);
            }
            else if (t.name.Equals("107"))
            {
                spriteStuff.Add("Hitmonchan", t);
            }
            else if (t.name.Equals("122"))
            {
                spriteStuff.Add("MrMime", t);
            }
            else if (t.name.Equals("129"))
            {
                spriteStuff.Add("Magikarp", t);
            }
            else if (t.name.Equals("131"))
            {
                spriteStuff.Add("Lapras", t);
            }
            else if (t.name.Equals("135"))
            {
                spriteStuff.Add("Jolteon", t);
            }
            else if (t.name.Equals("137"))
            {
                spriteStuff.Add("Porygon", t);
            }
            else if (t.name.Equals("150"))
            {
                spriteStuff.Add("Mewtwo", t);
            }
            else if (t.name.Equals("259"))
            {
                spriteStuff.Add("Marshtomp", t);
            }
            else if (t.name.Equals("330"))
            {
                spriteStuff.Add("Flygon", t);
            }
            else if (t.name.Equals("387"))
            {
                spriteStuff.Add("Turtwig", t);
            }
            else if (t.name.Equals("418"))
            {
                spriteStuff.Add("Buizel", t);
            }
            else if (t.name.Equals("448"))
            {
                spriteStuff.Add("Lucario", t);
            }
            else if (t.name.Equals("464"))
            {
                spriteStuff.Add("Rhyperior", t);
            }
            else if (t.name.Equals("053"))
            {
                spriteStuff.Add("Persian", t);
            }
            else if (t.name.Equals("248"))
            {
                spriteStuff.Add("Tyranitar", t);
            }
            else if (t.name.Equals("094"))
            {
                spriteStuff.Add("Gengar", t);
            }
            else if (t.name.Equals("700"))
            {
                spriteStuff.Add("Sylveon", t);
            }
            else if (t.name.Equals("567"))
            {
                spriteStuff.Add("Archeops", t);
            }
            else if (t.name.Equals("450"))
            {
                spriteStuff.Add("Hippowdon", t);
            }
            else if (t.name.Equals("376"))
            {
                spriteStuff.Add("Metagross", t);
            }
            else if (t.name.Equals("289"))
            {
                spriteStuff.Add("Slaking", t);
            }
            else if (t.name.Equals("242"))
            {
                spriteStuff.Add("Blissey", t);
            }
            else if (t.name.Equals("214"))
            {
                spriteStuff.Add("Heracross", t);
            }
            else if (t.name.Equals("197"))
            {
                spriteStuff.Add("Umbreon", t);
            }
            else if (t.name.Equals("034"))
            {
                spriteStuff.Add("Nidoking", t);
            }
        }


        return spriteStuff;
    }
}
