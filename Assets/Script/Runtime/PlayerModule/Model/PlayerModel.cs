﻿using System;
using Script.Runtime.CollectableModule.ItemModule.Model;
using Script.Runtime.WeaponModule;
using UnityEngine;

namespace Script.Runtime.PlayerModule.Model
{
    public class PlayerModel
    {
        public int MaxHealth;
        public int Health;
        public int Damage;
        public int Exp;
        public int Level;
        public int LevelIndex;

        public int MAX_LEVEL = 19;
        
        public float MovementSpeed;
        public float AttackRate;
        
        public int[] LevelExp;
        
        public PlayerWeaponSo DefaultWeapon;
        
        public PlayerWeaponSo CurrentPlayerWeaponSo;

        public readonly int AnimationHashIdle = Animator.StringToHash("Idle");
        public readonly int AnimationHashMove = Animator.StringToHash("Move");

        public PlayerModel(PlayerConfig config)
        {
            MaxHealth = config.MaxHealth;
            Health = config.MaxHealth;
            Damage = config.Damage;
            MovementSpeed = config.MovementSpeed;
            AttackRate = config.AttackRate;
            DefaultWeapon = config.DefaultWeapon;
            CurrentPlayerWeaponSo = config.DefaultWeapon;
            Exp = 0;
            Level = 1;
            LevelIndex = 0;
            LevelExp = config.LevelExp;
        }
    }
    
    [Serializable]
    public struct PlayerConfig
    {
        public int MaxHealth;
        public int Damage;
        
        public float MovementSpeed;
        public float AttackRate;
        
        public PlayerWeaponSo DefaultWeapon;
        
        public int[] LevelExp;
    }
}