﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using SCALE.Constants;
using SCALE.Enums;
using SCALE.Scripts.Managers;
using EventBus = SCALE.Events.EventBus;

namespace SCALE.Scripts.GameStates;

public partial class PlayingGameState : GameState
{
    public Node? scene;
    private EventBus _eventBus = null!;
    private StoreManager _storeManager = null!;

    public override void _EnterTree()
    {
        base._EnterTree();
        _eventBus = this.GetEventBus();
    }
    

    public PlayingGameState(GodotObject[]? args)
    {
        
    }

    public override void _Ready()
    {
        base._Ready();
        _eventBus.EmitOnGoToScene(Scenes.UI_DAYSTART_SCENE);
        _storeManager = Root.Tree.GetNode<StoreManager>($"/root/Root/StoreManager");
        _eventBus.EmitOnGoldCountChanged(_storeManager.Store.Gold);

        if (Root.Data.IsFirstInstructionsShown is false)
        {
            _eventBus.EmitOnPopupOpen(EPopupNames.InitialInstructions.ToString());
            Root.Data.IsFirstInstructionsShown = true;
        }
    }
    
    public override void _Input(InputEvent @event)
    {
        var eventBus = this.GetEventBus();

        if (Input.IsActionJustPressed(InputMappings.Escape_Key))
        {
            eventBus.EmitOnGoToGameState(EGameState.MainMenu.ToString());
            eventBus.EmitOnGoToScene(Scenes.GAMESTATES_MAIN_SCENE);
        }
    }
}