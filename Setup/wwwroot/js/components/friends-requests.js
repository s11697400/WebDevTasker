export default class FriendsRequests extends HTMLElement {
    shadowRoot;
templateId = 'friends-requests-tpl';
elementId = 'friends-requests';
friend;
constructor(friend) {
    super(); // always call super() first in the ctor.
    this.shadowRoot = this.attachShadow({mode: 'open'});
      this.state = {
        friend: friend,
    };
    this.friend = friend;
   
this.applyTemplate();
   
    this.setState('friend', friend);
}
 applyTemplate() {
    let template = document.getElementById(this.templateId);
    let clone = template.content.cloneNode(true);

    this.shadowRoot.appendChild(clone);
    const linkElem = document.createElement("link");
    linkElem.setAttribute("rel", "stylesheet");
    linkElem.setAttribute("href", "/css/friends.css");

    this.shadowRoot.appendChild(linkElem);
  
}
deleteRequest(){
    const output =  fetch('/api/Friendships/'+this.friend["friendshipId"], {
        method: 'DELETE'
    }).then(response => this.deleteSuccess(response));
}
deleteSuccess(response){
    console.log(response);
    if(response["ok"]){
       location.reload();
    }
}
connectedCallback() {
    

    console.log('item: connected to DOM');

    this.shadowRoot.querySelector('.friends-requests--container div[data-bind="name"]').textContent =  this.friend["user2"]["userName"];
    this.shadowRoot.querySelector('.friends-requests--container div[data-bind="score"]').textContent =  this.friend["user2"]["highScore"];
     
    this.applyEventlisteners();
}

disconnectedCallback() {
    console.log('item: disconnected from DOM');

}
AcceptRequest(){
    console.log("Accepteren");
    const output =  fetch('/api/Friendships/'+this.friend["friendshipId"], {
        method: 'PUT'
    }).then(response => this.deleteSuccess(response));
}
 applyEventlisteners() {
    // console.log('Click event in boodschappen-item.js nog niet ingeschakeld.');
    const self = this;
    this.addEventListener('click', () => {
    });
    console.log(this.shadowRoot);
    this.shadowRoot.querySelector('.friends-requests--container div[data-bind="delete"] button').addEventListener('click', () =>{

        this.deleteRequest();
        
    });
    this.shadowRoot.querySelector('.friends-requests--container div[data-bind="accept"] button').addEventListener('click', () =>{
        this.AcceptRequest();
    });
     
}
setState(key, value) {
    this.state[key] = value;
    this.updateBinding(key);
}

updateBinding(prop) {
    let bindings = this.shadowRoot.querySelectorAll(`[data-bind$="${prop}"]`);
    // https://developer.mozilla.org/en-US/docs/Web/API/HTMLElement/dataset
    bindings.forEach(node => {
        node.textContent = this.state[prop];
    })

}
}
customElements.define('friend-requests', FriendsRequests);

export {FriendsRequests};
