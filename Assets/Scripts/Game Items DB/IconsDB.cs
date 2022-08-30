using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconsDB : GameItemDB<Sprite>
{
    [SerializeField] private Sprite _movementSprite, _endTurnSprite, _meleeSprite, _shootSprite, _crouchSprite;

    public Sprite MoveSprite { get => _movementSprite; }
    public Sprite EndTurnSprite { get => _endTurnSprite; }
    public Sprite MeleeSprite { get => _meleeSprite; }
    public Sprite ShootSprite { get => _shootSprite; }
    public Sprite CrouchSprite { get => _crouchSprite; }
    public Sprite DefaultSprite { get => _defaultItem; }
}
