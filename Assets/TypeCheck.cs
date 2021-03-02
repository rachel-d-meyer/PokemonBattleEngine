using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TypeCheck : MonoBehaviour {


    public bool isSuperEffective(String moveType, Pokemon d)
    {

       
        String pType = d.Type;

        if (moveType.Equals("Water"))
        {
            if (pType.Equals("Fire") || pType.Equals("Ground") || pType.Equals("Rock"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Grass"))
        {
            if (pType.Equals("Water") || pType.Equals("Ground") || pType.Equals("Rock"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fire"))
        {
            if (pType.Equals("Ice") || pType.Equals("Steel") || pType.Equals("Bug") || pType.Equals("Grass"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Electric"))
        {
            if (pType.Equals("Water") || pType.Equals("Flying"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ice"))
        {
            if (pType.Equals("Grass") || pType.Equals("Ground") || pType.Equals("Flying") || pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fighting"))
        {
            if (pType.Equals("Normal") || pType.Equals("Rock") || pType.Equals("Ice") || pType.Equals("Dark") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Poison"))
        {
            if (pType.Equals("Grass") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ground"))
        {
            if (pType.Equals("Fire") || pType.Equals("Electric") || pType.Equals("Poison") || pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }

        }
        else if (moveType.Equals("Flying"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Bug") || pType.Equals("Grass"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Psychic"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Poison"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Bug"))
        {
            if (pType.Equals("Psychic") || pType.Equals("Dark") || pType.Equals("Grass"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Rock"))
        {
            if (pType.Equals("Flying") || pType.Equals("Fire") || pType.Equals("Ice") || pType.Equals("Bug"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ghost"))
        {
            if (pType.Equals("Ghost") || pType.Equals("Psychic"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dragon"))
        {
            if (pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dark"))
        {
            if (pType.Equals("Ghost") || pType.Equals("Psychic"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Steel"))
        {
            if (pType.Equals("Ice") || pType.Equals("Rock") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fairy"))
        {
            if (pType.Equals("Dragon") || pType.Equals("Dark") || pType.Equals("Fighting"))
            {
                return true;
            }
        }

        return false;
    }


    public bool isNotEffective(String moveType, Pokemon d)
    {
        
        String pType = d.Type;

        if (moveType.Equals("Water"))
        {
            if (pType.Equals("Water") || pType.Equals("Grass") || pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Grass"))
        {
            if (pType.Equals("Grass") || pType.Equals("Fire") || pType.Equals("Dragon") || pType.Equals("Poison") || pType.Equals("Bug") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fire"))
        {
            if (pType.Equals("Water") || pType.Equals("Fire") || pType.Equals("Dragon") || pType.Equals("Rock"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Electric"))
        {
            if (pType.Equals("Grass") || pType.Equals("Electric") || pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ice"))
        {
            if (pType.Equals("Fire") || pType.Equals("Water") || pType.Equals("Ice") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fighting"))
        {
            if (pType.Equals("Poison") || pType.Equals("Flying") || pType.Equals("Psychic") || pType.Equals("Bug") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Poison"))
        {
            if (pType.Equals("Poison") || pType.Equals("Ground") || pType.Equals("Rock") || pType.Equals("Ghost"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ground"))
        {
            if (pType.Equals("Grass") || pType.Equals("Bug"))
            {
                return true;
            }

        }
        else if (moveType.Equals("Flying"))
        {
            if (pType.Equals("Electric") || pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Psychic"))
        {
            if (pType.Equals("Psychic") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Bug"))
        {
            if (pType.Equals("Fire") || pType.Equals("Fighting") || pType.Equals("Flying") || pType.Equals("Poison") || pType.Equals("Ghost") || pType.Equals("Steel") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Rock"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Ground") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ghost"))
        {
            if (pType.Equals("Dark"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dragon"))
        {
            if (pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dark"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Dark") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Steel"))
        {
            if (pType.Equals("Fire") || pType.Equals("Water") || pType.Equals("Electric") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fairy"))
        {
            if (pType.Equals("Fire") || pType.Equals("Poison") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Normal"))
        {
            if (pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }
        }

        return false;
    }

    public bool isImmune(String m, Pokemon d)
    {
        if (d.Type.Equals("Fairy"))
        {
            if (m.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Ground"))
        {
            if (m.Equals("Electric"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Normal"))
        {
            if (m.Equals("Ghost"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Ghost"))
        {
            if (m.Equals("Normal") || m.Equals("Fighting"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Flying"))
        {
            if (m.Equals("Ground"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Dark"))
        {
            if (m.Equals("Psychic"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Steel"))
        {
            if (m.Equals("Poison"))
            {
                return true;
            }
        }


        return false;
    }
}
