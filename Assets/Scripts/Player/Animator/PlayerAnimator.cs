using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour{
    private static readonly int Horizontal = Animator.StringToHash(ANIMATOR_HORIZONTAL);
    private static readonly int Vertical = Animator.StringToHash(ANIMATOR_VERTICAL);

    [Header("Settings :")] [SerializeField]
    private Animator _playerAnimator;

    [SerializeField] private Player _player;

    public const string ANIMATOR_HORIZONTAL = "Horizontal";
    public const string ANIMATOR_VERTICAL = "Vertical";
}