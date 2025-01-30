using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class songClass
{
    public channel[] channels; // chanels : [ ... ]
    public class channel {
        public string type;
        public pattern[] patterns; // patterns : [ ... ]
        public class pattern {
            public note[] notes; // notes : [ ... ]
            public class note {
                public point[] points; // points : [ ... ]
                public class point {
                    public int tick;
                    public int volume;
                }
            }
        }
    }

}
