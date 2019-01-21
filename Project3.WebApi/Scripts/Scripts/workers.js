onmessage = function (e) {

    var syncFunc = function (ms) {
        var now = Date.now();
        while (Date.now() < (now + ms));
    };

    while (1) {
        syncFunc(10000);
        // Send request to server 
        postMessage([
            {
                id: 1,
                name: "ahihi"
            },
            {
                id: 2,
                name: "ahihi"
            },
            {
                id: 3,
                name: "ahihi"
            }
        ]);
    }
}