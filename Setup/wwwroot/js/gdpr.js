class GDPR {

    constructor() {
        this.showStatus();
        this.showContent();
        this.bindEvents();

        if (this.cookieStatus() !== 'accept') {
            this.showGDPR();
        } else {
            this.hideGDPR();
        }
    }

    bindEvents() {
        let buttonAccept = document.querySelector('.gdpr-consent__button--accept');
        buttonAccept.addEventListener('click', () => {
            this.cookieStatus('accept');
            this.showStatus();
            this.showContent();
            this.hideGDPR();
        });

        let buttonDeclined = document.querySelector('.gdpr-consent__button--reject');
        buttonDeclined.addEventListener('click', () => {
            this.cookieStatus('reject');
            this.showStatus();
            this.showContent();
            this.hideGDPR();
        });

//student uitwerking


    }


    showContent() {
        this.resetContent();
        const status = this.cookieStatus() == null ? 'not-chosen' : this.cookieStatus();
        const element = document.querySelector(`.content-gdpr-${status}`);
        if (element) {
            element.classList.add('show');
            element.classList.remove('hide');
        }

    }

    resetContent(){
        const classes = [
            '.content-gdpr-accept',

//student uitwerking

            '.content-gdpr-not-chosen'];

        for (const c of classes) {
            if (document.querySelector(c)) {
                document.querySelector(c).classList.add('hide');
                document.querySelector(c).classList.remove('show');
            }
        }
    }

    showStatus() {
        if (document.getElementById('content-gpdr-consent-status')) {
            document.getElementById('content-gpdr-consent-status').innerHTML = this.cookieStatus() == null ? 'Niet gekozen' : this.cookieStatus();

        }
    }

    cookieStatus(status) {
        if (status) {
            localStorage.setItem('gdpr-consent-choice', status);
          
            var today = new Date();
            var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
            var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
            
            let obj = {
                datum: date,
                tijd: time,
            };
            localStorage.setItem("gdpr-consent-data", JSON.stringify(obj) )

        }

//student uitwerking

        return localStorage.getItem('gdpr-consent-choice');
    }


//student uitwerking


    hideGDPR(){
        document.querySelector('.gdpr-consent').classList.add('hide');
        document.querySelector(`.gdpr-consent`).classList.remove('show');
    }

    showGDPR(){
        document.querySelector(`.gdpr-consent`).classList.add('show');
    }
}

const gdpr = new GDPR();