using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineRules
{

    public MachineRules()
    {


    }

    public Dictionary<string, string> getAnber()
    {
        return new Dictionary<string, string>{
            {"0a","XR1"},
            {"0b","YR4"},
            {"0Y","YR7"},
            {"0X","XR7"},
            {"1a","aR1"},
            {"1b","bR1"},
            {"1X","XL2"},
            {"1Y","YL2"},
            {"1β","βL2"},
            {"2a","XL3"},
            {"3a","aL3"},
            {"3b","bL3"},
            {"3X","XR0"},
            {"4a","aR4"},
            {"4b","bR4"},
            {"4X","XL5"},
            {"4Y","YL5"},
            {"4β","βL5"},
            {"5b","YL6"},
            {"6a","aL6"},
            {"6b","bL6"},
            {"6Y","YR0"},
            {"7X","XR7"},
            {"7Y","YR7"},
            {"7β","βR8"},
        };
    }

    public Dictionary<string, string> getAnita()
    {
        return new Dictionary<string, string>{
            {"0a","XR1"},
            {"0b","YR5"},
            {"0β","βR6"},
            {"1a","aR1"},
            {"1b","YR2"},
            {"1Z","ZL4"},
            {"1β","βL7"},
            {"2b","bR2"},
            {"2c","ZL3"},
            {"2Z","ZR2"},
            {"3b","bL3"},
            {"3Y","YR1"},
            {"3Z","ZL3"},
            {"4a","aL4"},
            {"4Y","bL4"},
            {"4X","XR0"},
            {"5b","YR5"},
            {"5Z","ZR5"},
            {"7X","XL7"},
            {"7a","XL7"},
            {"5β","βL6"},
            {"7β","βR6"},
        };
    }

    public Dictionary<string, string> getRehana()
    {
        return new Dictionary<string, string>{
            {"00","XR1"},
            {"10","0R1"},
            {"11","YR2"},
            {"1Y","YR1"},
            {"21","1L2"},
            {"2Y","YL2"},
            {"20","0L2"},
            {"2X","XR0"},
            {"2β","βL5"},
            {"30","XR4"},
            {"5Y","YL5"},
            {"5X","XR3"},
            {"50","0L5"},
            {"40","0R4"},
            {"4Y","YR4"},
            {"41","YR5"},
            {"4β","βL6"},
        };
    }

    public Dictionary<string, string> getPal()
    {
        return new Dictionary<string, string>{
            {"00","βR1"},
            {"01","βR2"},
            {"03","βR3"},
            {"0β","βL11"},
            {"10","0R4"},
            {"11","1R4"},
            {"13","3R4"},
            {"1β","βL11"},
            {"40","0R4"},
            {"41","1R4"},
            {"43","3R4"},
            {"4β","βL5"},
            {"50","βL6"},
            {"60","0L6"},
            {"61","1L6"},
            {"63","3L6"},
            {"6β","βR0"},
            {"20","0R7"},
            {"21","1R7"},
            {"23","3R7"},
            {"2β","βL11"},
            {"70","0R7"},
            {"71","1R7"},
            {"73","3R7"},
            {"7β","βL8"},
            {"81","βL6"},
            {"30","0R9"},
            {"31","1R9"},
            {"33","3R9"},
            {"3β","βL11"},
            {"90","0R9"},
            {"91","1R9"},
            {"93","3R9"},
            {"9β","βL10"},
            {"103","βL6"}
        };
    }


}
