// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Ke.Inputs
{
    public class @Controls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @Controls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""ThirdPersonPlayer"",
            ""id"": ""0c522026-f974-4f3a-8166-4a8979df69f0"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""64fede9f-a52e-44b6-9f50-eab00c06c1c8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""cbed0ff6-c81f-47b3-bd95-3598995a605d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuButton"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3106b70c-9892-4eb0-b286-30de6022613a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1dc102fb-b919-483c-931e-8eb8aa347537"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""RightStick"",
                    ""id"": ""3395909d-d890-4d7b-b602-05baa53ee5d5"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""75f3fc64-57db-4dd4-9ea0-a80f89e74dc1"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""69521832-87b9-4a30-b8ff-284576f0a3c0"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9ca7e1b9-5a2e-4750-9363-bba4ee6e794c"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6ac60ee2-4baa-472f-b1be-0b298fa7987d"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""37c142c4-0b8d-470e-8741-a66a311f1de1"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""83ae5b34-920c-40a5-a11b-c830fc1825f9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""50b26189-5f0e-49a5-9138-50ce22a6c630"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""75eb2c2f-5edf-4ec5-be87-dcc05d625548"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ddaff4c9-3f95-4e27-aad2-9bbc398e038f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""2f8818ec-f174-4120-a92f-c77a4765a87e"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d68335b0-a8c5-4562-a37f-101b7dfae734"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3dadb1da-e6ec-48eb-960c-152322e6ea8d"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8022a941-29ae-43e0-82e0-bc30b38bb3e3"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b6ba3b98-6e18-44e6-8d8b-a1178e1cd0f8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""7ae8b8b1-9dc5-4844-b6b2-dc0cd2e38d85"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""438abfea-993c-441b-bf1e-5b46829b9512"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bc1082b8-ff32-468c-8362-58814fa2c516"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""783909f2-6b80-49fb-a898-af58ba893731"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""53cd2e2a-954b-490a-a23d-18f11f0b972b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""52dc3b93-fc21-429f-8942-83b5998c23aa"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""80101117-4d8f-42c5-ba5b-bf63c1185f45"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RealTimeStrategy"",
            ""id"": ""e2069743-bf56-4bd2-9ce0-fb46c40fa94c"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""2b065d56-e2c5-4d43-a54c-9fed0007d58a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""InvertVector2(invertX=false,invertY=false)"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""64b0b807-2f93-4056-8d28-4e4aad06c074"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""39f25a55-3390-4870-9f49-e40f68751c55"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MovementAccelerate"",
                    ""type"": ""Button"",
                    ""id"": ""251fdf10-0128-44ef-8341-4eb75793d50b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateButton"",
                    ""type"": ""Button"",
                    ""id"": ""98f66615-ceac-4458-8904-f59376b7ac70"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MenuButton"",
                    ""type"": ""Button"",
                    ""id"": ""96195e66-a5d5-4ce7-a449-30fe73577426"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""ae6429dc-3fca-4bdc-bc72-004d83185348"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectDouble"",
                    ""type"": ""Button"",
                    ""id"": ""18a12a3d-37c1-4732-8d66-403db7225a6c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MultipleSelect"",
                    ""type"": ""Button"",
                    ""id"": ""2606085b-1f60-4597-81fd-b6ad94dac56a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Deselect"",
                    ""type"": ""Button"",
                    ""id"": ""27cb54a9-6f27-4276-ab35-90657c6b08c1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""UnitMovement"",
                    ""type"": ""Button"",
                    ""id"": ""0f5f2448-98b8-47cd-b099-6063ee19bec2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""17d01907-bdce-4a3b-bd0f-6aa6000d5758"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""641982d8-795a-4ad3-b3a2-f69bd9cf5de2"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""104e484a-f3c9-4519-8f0f-d8c9afe850c8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""6c705bf4-966e-4f73-b40b-2c2e3df03669"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""103da8b0-4043-45dd-9d6f-357d608268ad"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""803bacd3-0636-43cd-a8d5-e02496c87d86"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""ArrowKeys"",
                    ""id"": ""ae6a4321-b247-42bd-805e-9f956dd3b4df"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""99cc0f67-797e-4a3a-b329-260008aff3a6"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7d1e27d8-438a-47df-b1ad-bd580b160a67"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f93e30bf-1c03-40b0-ab09-f2ae7e897e81"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6e7398d9-cc9f-4150-9f3e-3c379e88d8f8"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""LeftStick"",
                    ""id"": ""7a47cd79-92ac-4da0-b18b-542fb16d872e"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c7944f4-8f80-4693-afbd-3a334fc42004"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ba79a89b-44ee-4216-8186-52b1ad69cdeb"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e84ba583-206e-4040-90a8-1472c4fecd5e"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b349441a-58d1-4c83-90df-c81ef1ebb43e"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""120d2fee-4025-4f62-8fbf-4eb65d2b1010"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""RightStick"",
                    ""id"": ""4a7d40dc-3787-4994-90a4-3de3d2701466"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""763a64c6-c73e-4f80-9435-2649f2bd7cc0"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""eeb8cf4f-1fe7-4c82-8776-2523fb5283c1"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5f9589bb-440a-45b0-ace3-94fd5bf9d06a"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""031762ee-23cc-4b09-a3c1-771a172e31bd"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""9ef21230-06fb-4b06-822a-8207f78e2b94"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""RotateButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0095e840-8539-4f5a-b90c-4cf061b51df4"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""RotateButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0af25eec-b0f5-460c-a9b3-9600efa3ca50"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c3ca8f3b-b5f6-4f87-b4fd-e7793001a7fd"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""MenuButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfb48770-3880-4cdf-9a8c-43d51611a2ff"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MovementAccelerate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""236d3257-ff08-49f7-afa6-058654cb00ed"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""87ad7811-72ff-4391-bb17-5e4ab717a635"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fffbe5c3-bd38-40a5-b39a-e61a94f0978b"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""MultipleSelect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""13986532-bee4-455f-9775-4978bca5c2aa"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""Deselect"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca2fc4f2-4e31-4ae1-b733-e7ae6b981c6b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""MultiTap(tapTime=0.09,tapDelay=0.15,pressPoint=0.15)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""SelectDouble"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""969ed1df-b3e3-4dd0-8104-7e9053c75447"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": ""MultiTap"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectDouble"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b820530b-8359-4436-81b0-122e1ccd4133"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and Mouse"",
                    ""action"": ""UnitMovement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard and Mouse"",
            ""bindingGroup"": ""Keyboard and Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // ThirdPersonPlayer
            m_ThirdPersonPlayer = asset.FindActionMap("ThirdPersonPlayer", throwIfNotFound: true);
            m_ThirdPersonPlayer_Look = m_ThirdPersonPlayer.FindAction("Look", throwIfNotFound: true);
            m_ThirdPersonPlayer_Movement = m_ThirdPersonPlayer.FindAction("Movement", throwIfNotFound: true);
            m_ThirdPersonPlayer_MenuButton = m_ThirdPersonPlayer.FindAction("MenuButton", throwIfNotFound: true);
            // RealTimeStrategy
            m_RealTimeStrategy = asset.FindActionMap("RealTimeStrategy", throwIfNotFound: true);
            m_RealTimeStrategy_Rotate = m_RealTimeStrategy.FindAction("Rotate", throwIfNotFound: true);
            m_RealTimeStrategy_Zoom = m_RealTimeStrategy.FindAction("Zoom", throwIfNotFound: true);
            m_RealTimeStrategy_Movement = m_RealTimeStrategy.FindAction("Movement", throwIfNotFound: true);
            m_RealTimeStrategy_MovementAccelerate = m_RealTimeStrategy.FindAction("MovementAccelerate", throwIfNotFound: true);
            m_RealTimeStrategy_RotateButton = m_RealTimeStrategy.FindAction("RotateButton", throwIfNotFound: true);
            m_RealTimeStrategy_MenuButton = m_RealTimeStrategy.FindAction("MenuButton", throwIfNotFound: true);
            m_RealTimeStrategy_Select = m_RealTimeStrategy.FindAction("Select", throwIfNotFound: true);
            m_RealTimeStrategy_SelectDouble = m_RealTimeStrategy.FindAction("SelectDouble", throwIfNotFound: true);
            m_RealTimeStrategy_MultipleSelect = m_RealTimeStrategy.FindAction("MultipleSelect", throwIfNotFound: true);
            m_RealTimeStrategy_Deselect = m_RealTimeStrategy.FindAction("Deselect", throwIfNotFound: true);
            m_RealTimeStrategy_UnitMovement = m_RealTimeStrategy.FindAction("UnitMovement", throwIfNotFound: true);
        }

        public void Dispose()
        {
            UnityEngine.Object.Destroy(asset);
        }

        public InputBinding? bindingMask
        {
            get => asset.bindingMask;
            set => asset.bindingMask = value;
        }

        public ReadOnlyArray<InputDevice>? devices
        {
            get => asset.devices;
            set => asset.devices = value;
        }

        public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

        public bool Contains(InputAction action)
        {
            return asset.Contains(action);
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return asset.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Enable()
        {
            asset.Enable();
        }

        public void Disable()
        {
            asset.Disable();
        }

        // ThirdPersonPlayer
        private readonly InputActionMap m_ThirdPersonPlayer;
        private IThirdPersonPlayerActions m_ThirdPersonPlayerActionsCallbackInterface;
        private readonly InputAction m_ThirdPersonPlayer_Look;
        private readonly InputAction m_ThirdPersonPlayer_Movement;
        private readonly InputAction m_ThirdPersonPlayer_MenuButton;
        public struct ThirdPersonPlayerActions
        {
            private @Controls m_Wrapper;
            public ThirdPersonPlayerActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Look => m_Wrapper.m_ThirdPersonPlayer_Look;
            public InputAction @Movement => m_Wrapper.m_ThirdPersonPlayer_Movement;
            public InputAction @MenuButton => m_Wrapper.m_ThirdPersonPlayer_MenuButton;
            public InputActionMap Get() { return m_Wrapper.m_ThirdPersonPlayer; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(ThirdPersonPlayerActions set) { return set.Get(); }
            public void SetCallbacks(IThirdPersonPlayerActions instance)
            {
                if (m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface != null)
                {
                    @Look.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnLook;
                    @Movement.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMovement;
                    @MenuButton.started -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMenuButton;
                    @MenuButton.performed -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMenuButton;
                    @MenuButton.canceled -= m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface.OnMenuButton;
                }
                m_Wrapper.m_ThirdPersonPlayerActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @MenuButton.started += instance.OnMenuButton;
                    @MenuButton.performed += instance.OnMenuButton;
                    @MenuButton.canceled += instance.OnMenuButton;
                }
            }
        }
        public ThirdPersonPlayerActions @ThirdPersonPlayer => new ThirdPersonPlayerActions(this);

        // RealTimeStrategy
        private readonly InputActionMap m_RealTimeStrategy;
        private IRealTimeStrategyActions m_RealTimeStrategyActionsCallbackInterface;
        private readonly InputAction m_RealTimeStrategy_Rotate;
        private readonly InputAction m_RealTimeStrategy_Zoom;
        private readonly InputAction m_RealTimeStrategy_Movement;
        private readonly InputAction m_RealTimeStrategy_MovementAccelerate;
        private readonly InputAction m_RealTimeStrategy_RotateButton;
        private readonly InputAction m_RealTimeStrategy_MenuButton;
        private readonly InputAction m_RealTimeStrategy_Select;
        private readonly InputAction m_RealTimeStrategy_SelectDouble;
        private readonly InputAction m_RealTimeStrategy_MultipleSelect;
        private readonly InputAction m_RealTimeStrategy_Deselect;
        private readonly InputAction m_RealTimeStrategy_UnitMovement;
        public struct RealTimeStrategyActions
        {
            private @Controls m_Wrapper;
            public RealTimeStrategyActions(@Controls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Rotate => m_Wrapper.m_RealTimeStrategy_Rotate;
            public InputAction @Zoom => m_Wrapper.m_RealTimeStrategy_Zoom;
            public InputAction @Movement => m_Wrapper.m_RealTimeStrategy_Movement;
            public InputAction @MovementAccelerate => m_Wrapper.m_RealTimeStrategy_MovementAccelerate;
            public InputAction @RotateButton => m_Wrapper.m_RealTimeStrategy_RotateButton;
            public InputAction @MenuButton => m_Wrapper.m_RealTimeStrategy_MenuButton;
            public InputAction @Select => m_Wrapper.m_RealTimeStrategy_Select;
            public InputAction @SelectDouble => m_Wrapper.m_RealTimeStrategy_SelectDouble;
            public InputAction @MultipleSelect => m_Wrapper.m_RealTimeStrategy_MultipleSelect;
            public InputAction @Deselect => m_Wrapper.m_RealTimeStrategy_Deselect;
            public InputAction @UnitMovement => m_Wrapper.m_RealTimeStrategy_UnitMovement;
            public InputActionMap Get() { return m_Wrapper.m_RealTimeStrategy; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(RealTimeStrategyActions set) { return set.Get(); }
            public void SetCallbacks(IRealTimeStrategyActions instance)
            {
                if (m_Wrapper.m_RealTimeStrategyActionsCallbackInterface != null)
                {
                    @Rotate.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnRotate;
                    @Rotate.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnRotate;
                    @Rotate.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnRotate;
                    @Zoom.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnZoom;
                    @Zoom.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnZoom;
                    @Zoom.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnZoom;
                    @Movement.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMovement;
                    @MovementAccelerate.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMovementAccelerate;
                    @MovementAccelerate.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMovementAccelerate;
                    @MovementAccelerate.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMovementAccelerate;
                    @RotateButton.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnRotateButton;
                    @RotateButton.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnRotateButton;
                    @RotateButton.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnRotateButton;
                    @MenuButton.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMenuButton;
                    @MenuButton.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMenuButton;
                    @MenuButton.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMenuButton;
                    @Select.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnSelect;
                    @Select.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnSelect;
                    @Select.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnSelect;
                    @SelectDouble.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnSelectDouble;
                    @SelectDouble.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnSelectDouble;
                    @SelectDouble.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnSelectDouble;
                    @MultipleSelect.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMultipleSelect;
                    @MultipleSelect.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMultipleSelect;
                    @MultipleSelect.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnMultipleSelect;
                    @Deselect.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnDeselect;
                    @Deselect.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnDeselect;
                    @Deselect.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnDeselect;
                    @UnitMovement.started -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnUnitMovement;
                    @UnitMovement.performed -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnUnitMovement;
                    @UnitMovement.canceled -= m_Wrapper.m_RealTimeStrategyActionsCallbackInterface.OnUnitMovement;
                }
                m_Wrapper.m_RealTimeStrategyActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Rotate.started += instance.OnRotate;
                    @Rotate.performed += instance.OnRotate;
                    @Rotate.canceled += instance.OnRotate;
                    @Zoom.started += instance.OnZoom;
                    @Zoom.performed += instance.OnZoom;
                    @Zoom.canceled += instance.OnZoom;
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @MovementAccelerate.started += instance.OnMovementAccelerate;
                    @MovementAccelerate.performed += instance.OnMovementAccelerate;
                    @MovementAccelerate.canceled += instance.OnMovementAccelerate;
                    @RotateButton.started += instance.OnRotateButton;
                    @RotateButton.performed += instance.OnRotateButton;
                    @RotateButton.canceled += instance.OnRotateButton;
                    @MenuButton.started += instance.OnMenuButton;
                    @MenuButton.performed += instance.OnMenuButton;
                    @MenuButton.canceled += instance.OnMenuButton;
                    @Select.started += instance.OnSelect;
                    @Select.performed += instance.OnSelect;
                    @Select.canceled += instance.OnSelect;
                    @SelectDouble.started += instance.OnSelectDouble;
                    @SelectDouble.performed += instance.OnSelectDouble;
                    @SelectDouble.canceled += instance.OnSelectDouble;
                    @MultipleSelect.started += instance.OnMultipleSelect;
                    @MultipleSelect.performed += instance.OnMultipleSelect;
                    @MultipleSelect.canceled += instance.OnMultipleSelect;
                    @Deselect.started += instance.OnDeselect;
                    @Deselect.performed += instance.OnDeselect;
                    @Deselect.canceled += instance.OnDeselect;
                    @UnitMovement.started += instance.OnUnitMovement;
                    @UnitMovement.performed += instance.OnUnitMovement;
                    @UnitMovement.canceled += instance.OnUnitMovement;
                }
            }
        }
        public RealTimeStrategyActions @RealTimeStrategy => new RealTimeStrategyActions(this);
        private int m_KeyboardandMouseSchemeIndex = -1;
        public InputControlScheme KeyboardandMouseScheme
        {
            get
            {
                if (m_KeyboardandMouseSchemeIndex == -1) m_KeyboardandMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and Mouse");
                return asset.controlSchemes[m_KeyboardandMouseSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface IThirdPersonPlayerActions
        {
            void OnLook(InputAction.CallbackContext context);
            void OnMovement(InputAction.CallbackContext context);
            void OnMenuButton(InputAction.CallbackContext context);
        }
        public interface IRealTimeStrategyActions
        {
            void OnRotate(InputAction.CallbackContext context);
            void OnZoom(InputAction.CallbackContext context);
            void OnMovement(InputAction.CallbackContext context);
            void OnMovementAccelerate(InputAction.CallbackContext context);
            void OnRotateButton(InputAction.CallbackContext context);
            void OnMenuButton(InputAction.CallbackContext context);
            void OnSelect(InputAction.CallbackContext context);
            void OnSelectDouble(InputAction.CallbackContext context);
            void OnMultipleSelect(InputAction.CallbackContext context);
            void OnDeselect(InputAction.CallbackContext context);
            void OnUnitMovement(InputAction.CallbackContext context);
        }
    }
}
