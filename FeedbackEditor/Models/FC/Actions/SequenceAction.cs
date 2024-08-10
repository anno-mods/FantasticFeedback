using Accessibility;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace FeedbackEditor.Models.FC.Actions
{
    public enum ActionType {
        WALK_BETWEEN_DUMMIES = 0,
        PLAY_SEQUENCE = 1,
        WAIT = 2,
        BRANCH = 3,
        WALK_SPLINE = 4,
        FADE = 6,
        BARRIER = 7,
        SCALE = 9,
        TURN_TO = 10,
        PLAY_ANY_SEQUENCE = 12,
        FOLLOW_SPLINE_ANY_SEQUENCE = 15
    }

    public enum SequenceID
    {
        undefined = -1,
        @default = 0,
        idle01 = 1000,
        idle02 = 1001,
        idle03 = 1002,
        idle04 = 1003,
        idle05 = 1003,
        death01 = 1005,
        talk01 = 1010,
        talk02 = 1011,
        greet01 = 1020,
        bow01 = 1021,
        cheer01 = 1030,
        cheer02 = 1031,
        cheer03 = 1032,
        lookat01 = 1040,
        lookat02 = 1041,
        protest01 = 1050,
        protest02 = 1051,
        protest03 = 1052,
        protest04 = 1053,
        protest05 = 1054,
        protest06 = 1055,
        laydown01 = 1060,
        laydown02 = 1061,
        laydown03 = 1062,
        fishing01 = 1070,
        fishing02 = 1071,
        fishing03 = 1072,
        dance01 = 1080,
        dance02 = 1081,
        dance03 = 1082,
        dance04 = 1083,
        fight01 = 1090,
        fight02 = 1091,
        fight03 = 1092,
        fight04 = 1093,
        fight05 = 1094,
        walk01 = 2000,
        walk02 = 2001,
        walk03 = 2002,
        walk04 = 2003,
        walk05 = 2004,
        walk06 = 2005,
        walk07 = 2005,
        drunkenwalk01 = 2010,
        drunkenwalk02 = 2011,
        run01 = 2100,
        panicrun01 = 2101,
        panicrun02 = 2102,
        riotspecial01 = 5350,
        riotspecial02 = 5351,
        riotspecial03 = 5352,
        donate01 = 2200,
        buy01 = 2201,
        buy02 = 2202,
        work01 = 3000,
        work02 = 3001,
        work03 = 3002,
        work04 = 3003,
        work05 = 3004,
        work06 = 3005,
        work07 = 3006,
        work08 = 3007,
        work09 = 3008,
        work10 = 3009,
        work11 = 3020,
        work12 = 3021,
        work13 = 3022,
        work14 = 3023,
        work15 = 3024,
        work16 = 3025,
        work17 = 3026,
        work18 = 3027,
        work19 = 3028,
        stand01 = 4000,
        build01 = 5000,
        extFire01 = 5100,
        extFire02 = 5101,
        extFire03 = 5102,
        pray01 = 5200,
        protestwalk01 = 5300,
        protestwalk02 = 5301,
        protestwalk03 = 5302,
        takeoff01 = 5400,
        land01 = 5410,
        portrait_neutral_idle = 10000,
        portrait_neutral_talk = 10001,
        portrait_friendly_idle = 10010,
        portrait_friendly_talk = 10011,
        portrait_angry_idle = 10020,
        portrait_angry_talk = 10021,
        portrait_neutral_talk_idle = 10030,
        portrait_friendly_talk_idle = 10031,
        portrait_angry_talk_idle = 10040,
        work_staged01 = 3010,
        work_staged02 = 3011,
        work_staged03 = 3012,
        boosted = 3050,
        sitdown01 = 5500,
        sitdown02 = 5501,
        sitdown03 = 5502,
        explode01 = 2300,
        explode02 = 2301,
        explode03 = 2302,
        explode04 = 2303,
        idleLoaded01 = 6000,
        walkingLoaded01 = 6001,
        hitwood = 2400,
        hitbrick = 2401,
        hitsteel = 2402,
        hitconcrete = 2403,
        misswater = 2410,
        missland = 2411,
    }

    [XmlInclude(typeof(PlaySequenceAction))]
    [XmlInclude(typeof(WalkBetweenDummiesAction))]
    [XmlRoot("i")]
    public class SequenceAction
    {
        [XmlElement(ElementName = "hasValue")]
        public bool HasValue { get; set; }

        [XmlIgnore]
        public virtual ActionType ElementType { get; set; }

        [XmlElement(ElementName = "elementType")]
        public int ElementTypeForSerialization
        {
            get => (int)ElementType;
            set => ElementType = (ActionType)value;
        }

        [XmlIgnore]
        public IEnumerable<SequenceID> SequenceIDValues { get; set; }

        public SequenceAction() {
            SequenceIDValues = Enum.GetValues<SequenceID>().Cast<SequenceID>().ToList();
        }
    }

    public class SequenceActionFactory
    {
        public Type GetTypeOfAction(ActionType actionType)
        {
            return actionType switch
            {
                ActionType.WALK_BETWEEN_DUMMIES => typeof(WalkBetweenDummiesAction),
                ActionType.PLAY_SEQUENCE => typeof(PlaySequenceAction),
                ActionType.BRANCH => typeof(BranchAction),
                ActionType.PLAY_ANY_SEQUENCE => typeof(PlayAnySequenceAction),
                ActionType.FADE => typeof(FadeAction),
                _ => typeof(SequenceAction)
            };
        }
    }
}
