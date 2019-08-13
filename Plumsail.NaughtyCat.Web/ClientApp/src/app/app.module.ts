import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { TopComponent } from "./components/top/top.component";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { MatButtonModule } from "@angular/material/button";
import { MatCheckboxModule } from "@angular/material/checkbox";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatInputModule } from "@angular/material";
import { RabbitListviewComponent } from "./components/rabbit/rabbit-listview/rabbit-listview.component";
import { HomeComponent } from "./components/home/home.component";
import { LoginComponent } from "./components/login/login.component";
import { FormsModule, NgControl, ReactiveFormsModule } from "@angular/forms";
import { BottomComponent } from "./components/bottom/bottom.component";
import { AccountService } from "./services/account.service";
import { HttpClientModule } from "@angular/common/http";
import { RegisterComponent } from "./components/register/register.component";

@NgModule({
  declarations: [
    AppComponent,
    TopComponent,
    RabbitListviewComponent,
    HomeComponent,
    LoginComponent,
    BottomComponent,
    RegisterComponent
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
    MatInputModule
  ],
  providers: [AccountService],
  bootstrap: [AppComponent]
})
export class AppModule {}
