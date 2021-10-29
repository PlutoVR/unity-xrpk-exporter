const MAMInterface = {
        MAMGetAppId: function () {
            if (!window.AppState) return;
            var returnStr = window.AppState.appId;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1;
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        },

        MAMGetInitialPosition: function () {
            if (!window.AppState) return;
            var returnStr = window.AppState.initialPosition;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1;
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        },

        MAMGetModelLoader: function () {
            if (!window.AppState) return;
            var returnStr = window.AppState.modelLoader;
            var buffer = _malloc(lengthBytesUTF8(returnStr) + 1);
            var bufferSize = lengthBytesUTF8(returnStr) + 1;
            stringToUTF8(returnStr, buffer, bufferSize);
            return buffer;
        },

        /**
         * WORK IN PROGRESS
         * @param appUrl
         * @param transform
         */
        MAMLaunchApp: function (appUrl, transform) {
            if (!window.AppState) return;
            console.log("** launching ** ", appUrl);
            window.parent.dispatchEvent(
                new CustomEvent("plutomae-xrpk-event", {
                    detail: {
                        type: "launch-xrpk",
                        appId: window.AppState.appId,
                        url: Pointer_stringify(appUrl),
                        transform: JSON.parse(Pointer_stringify(transform)),
                    },
                })
            );
        },

        MAMLaunchAppByNameId: function (name, id, transform) {
            if (!window.AppState) return;

            console.log("** launching ** ", name);

            window.parent.dispatchEvent(
                new CustomEvent("plutomae-xrpk-event", {
                    detail: {
                        type: "launch-xrpk-by-name-id",
                        name: Pointer_stringify(name),
                        uuid: Pointer_stringify(id),
                        transform: JSON.parse(Pointer_stringify(transform)),
                    },
                })
            );
        },

        MAMGetApps: function () {
            if (!window.AppState) return;

            console.log("### in MAMGetApps")
            console.log("### unityInstance ", unityInstance)
            console.log("### window parent unityInstance ", window.parent.unityInstance)

            const uuid = Math.random().toString()
            const listener = function (e) {
                console.log("### in listener :: pre")
                if (e.detail.responseId !== uuid) return;
                console.log("### in listener :: post")
                if (!unityInstance) {
                    console.log("no instance -- aborting")
                    return
                }
                const data = JSON.stringify(e.detail.data)
                const error = JSON.stringify(e.detail.error)

                if (data) unityInstance.SendMessage("MultiAppListener", "HandleAppListData", data);
                if (error) unityInstance.SendMessage("MultiAppListener", "HandleAppListError", error);
            }

            window.parent.addEventListener("plutomae-event-promise-response", listener)

            const toDispatch = new CustomEvent("plutomae-xrpk-event", {
                detail: {
                    type: "get-saved-xrpks",
                    appId: window.AppState.appId,
                    responseId: uuid
                },
            })

            console.log("### dispatching: ", toDispatch);

            window.parent.dispatchEvent(toDispatch);
        }

};

mergeInto(LibraryManager.library, MAMInterface);
