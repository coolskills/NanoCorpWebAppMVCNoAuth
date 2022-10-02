(function () {
    const connection = new signalR.HubConnectionBuilder().withUrl("/hub").build();

    const ccJob = "ConcurrentJobs";
    const ncJob = "NonConcurrentJobs";
    const ccDom = firstLetterLowerCase(ccJob);
    const ncDom = firstLetterLowerCase(ncJob);
    const ccCnt = ccDom + 'Count';
    const ncCnt = ncDom + 'Count';
    const ccHdr = ccDom + 'Header';
    const ncHdr = ncDom + 'Header';

    async function start() {
        try {
            await connection.start();
            console.log("SignalR Connected.");
        } catch (err) {
            console.log(err);
            setTimeout(start, 5000);
        }
    };

    connection.onclose(async () => {
        await start();
    });

    start();

    function firstLetterLowerCase(title) {
        return title.charAt(0).toLowerCase() + title.substr(1);
    }

    function createJobElement(eDom, eTitle, message, eCnt, eHdr) {
        let li = document.createElement("li");
        let list = document.getElementById(eDom);
        if (!list.hasChildNodes()) {
            document.getElementById(eHdr).innerHTML = eTitle;
        }
        list.insertBefore(li, list.childNodes[0]);

        li.textContent = `${message}`;

        let cnt = document.getElementById(eCnt);
        cnt.innerHTML = list.childNodes.length.toString();
    }

    connection.on(ccJob, function (message) {
        createJobElement(ccDom, `*** ${ccJob} Jobs ***`, message, ccCnt, ccHdr);
    });

    connection.on(ncJob, function (message) {
        createJobElement(ncDom, `*** ${ncJob} Jobs ***`, message, ncCnt, ncHdr);
    });
})();
