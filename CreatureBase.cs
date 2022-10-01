using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace CreatureTable_Converter
{
    [DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
    public class CreatureBase
    {
        public string Name;

        public List<Int32> SpriteTypes;

        public bool bMale;

        public CREATURETRIBE CreatureTribe;

        public byte MoveTimes;
        public byte MoveRatio;
        public byte MoveTimesMotor;

        public Int32 Height;
        public Int32 Width;

        public Int32 DeadHeight;

        public UInt16 DeadActionInfo;

        public Int32 ColorSet;

        public bool bFlyingCreature;

        public Int32 FlyingHeight;

        public Int32 bHeadCut;

        public Int32 HPBarWidth;

        public UInt16 ChangeColorSet;

        public UInt16 ShadowCount;

        public Int32 EffectStatus;

        public Int32 Level;

        public UInt16[] ActionSound;

        public Int32[] ActionCount;

        public ItemWearInfo ItemWearInfo;

        public bool bFade;

        public bool bFadeShadow;

        public byte[] NewValue668;

        public int GetActionMax()
        {
            if (this.CreatureTribe == CREATURETRIBE.SLAYER || this.CreatureTribe == CREATURETRIBE.SLAYER_NPC)
                return (int)ACTION_SLAYER.MAX;
            else if (this.CreatureTribe == CREATURETRIBE.VAMPIRE || this.CreatureTribe == CREATURETRIBE.NPC || this.CreatureTribe >= CREATURETRIBE.MAX)
            {
                if (this.SpriteTypes[0] == 90)
                    return (int)ACTION_OUSTERS.MAX;
                return (int)ACTION_VAMPIRE.MAX;
            }
            else if (this.CreatureTribe == CREATURETRIBE.OUSTERS || this.CreatureTribe == CREATURETRIBE.OUSTERS_NPC)
                return (int)ACTION_OUSTERS.MAX;

            return 0;
        }

        public void SaveToFile(ref FileStream file)
        {
            //Name
            BinarySerializer.SerializeString(ref file, EncodingHelper.encName, this.Name);

            //SpriteTypes
            BinarySerializer.SerializeInt32(ref file, this.SpriteTypes.Count);
            foreach (Int32 i in this.SpriteTypes)
                BinarySerializer.SerializeInt32(ref file, i);

            //bMale
            BinarySerializer.SerializeBool(ref file, this.bMale);

            //CreatureTribe
            BinarySerializer.SerializeByte(ref file, (byte)this.CreatureTribe);

            //MoveTimes
            BinarySerializer.SerializeByte(ref file, this.MoveTimes);

            //MoveRatio
            BinarySerializer.SerializeByte(ref file, this.MoveRatio);

            //MoveTimesMotor
            BinarySerializer.SerializeByte(ref file, this.MoveTimesMotor);

            //Height, Width
            BinarySerializer.SerializeInt32(ref file, this.Height);
            BinarySerializer.SerializeInt32(ref file, this.Width);

            //DeadHeight
            BinarySerializer.SerializeInt32(ref file, this.DeadHeight);

            //DeadActionInfo
            BinarySerializer.SerializeUInt16(ref file, this.DeadActionInfo);

            //ColorSet
            BinarySerializer.SerializeInt32(ref file, this.ColorSet);

            //bFlyingCreature
            BinarySerializer.SerializeBool(ref file, this.bFlyingCreature);

            //FlyingHeight
            BinarySerializer.SerializeInt32(ref file, this.FlyingHeight);

            //bHeadCut
            BinarySerializer.SerializeInt32(ref file, this.bHeadCut);

            //HPBarWidth
            BinarySerializer.SerializeInt32(ref file, this.HPBarWidth);

            //ChangeColorSet
            BinarySerializer.SerializeUInt16(ref file, this.ChangeColorSet);

            //ShadowCount
            BinarySerializer.SerializeUInt16(ref file, this.ShadowCount);

            //EffectStatus
            BinarySerializer.SerializeInt32(ref file, this.EffectStatus);

            //Level
            BinarySerializer.SerializeInt32(ref file, this.Level);

            //ActionSound
            foreach (UInt16 i in this.ActionSound)
                BinarySerializer.SerializeUInt16(ref file, i);

            //ActionCount
            foreach (Int32 i in this.ActionCount)
                BinarySerializer.SerializeInt32(ref file, i);

            //ItemWearInfo
            if (this.ItemWearInfo == null) BinarySerializer.SerializeBool(ref file, false);
            else
            {
                BinarySerializer.SerializeBool(ref file, true);
                this.ItemWearInfo.SaveToFile(ref file);
            }

            //bFade
            BinarySerializer.SerializeBool(ref file, this.bFade);

            //bFadeShadow
            BinarySerializer.SerializeBool(ref file, this.bFadeShadow);

            //NewValue668
            for (int i = 0; i < 8; i++)
                BinarySerializer.SerializeByte(ref file, this.NewValue668[i]);
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}