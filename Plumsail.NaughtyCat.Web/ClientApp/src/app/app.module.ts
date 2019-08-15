import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { TopComponent } from "./components/top/top.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import {
  MatInputModule,
  MatExpansionModule,
  MatListModule,
  MatDividerModule
} from "@angular/material";
import { RabbitListviewComponent } from "./components/rabbit/rabbit-listview/rabbit-listview.component";
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/login/login.component";
import { FormsModule, NgControl, ReactiveFormsModule } from "@angular/forms";
import { BottomComponent } from "./components/bottom/bottom.component";
import { AuthService } from "./services/auth.service";
import { HttpClientModule } from "@angular/common/http";
import { RegisterComponent } from "./components/register/register.component";
import { JwtModule } from "@auth0/angular-jwt";
import { tokenGetter } from "./helpers/auth-helpers";
import { RabbitMainComponent } from "./components/rabbit/rabbit-main/rabbit-main.component";
import { RabbitEditComponent } from "./components/rabbit/rabbit-edit/rabbit-edit.component";

@NgModule({
  declarations: [
    AppComponent,
    TopComponent,
    RabbitListviewComponent,
    HomeComponent,
    LoginComponent,
    BottomComponent,
    RegisterComponent,
    RabbitMainComponent,
    RabbitEditComponent
  ],
  imports: [
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatDividerModule,
    MatExpansionModule,
    MatListModule,
    MatInputModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: tokenGetter,
        whitelistedDomains: [window.location.origin],
        blacklistedRoutes: []
      }
    })
  ],
  providers: [AuthService],
  bootstrap: [AppComponent]
})
export class AppModule {}
