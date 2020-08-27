import { Component, OnInit } from '@angular/core';
import {AuthenticationClient} from "../../api-client/authentication/clients";

@Component({
  selector: 'app-authentication',
  templateUrl: './authentication.component.html',
  styleUrls: ['./authentication.component.scss'],
})
export class AuthenticationComponent implements OnInit {

  constructor(private authClient: AuthenticationClient) { }

  ngOnInit() {}

}
