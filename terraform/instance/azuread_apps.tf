resource "random_uuid" "api_permission_scope" {
}

resource "azuread_application" "api" {
  display_name     = "${var.name}-api"
  identifier_uris  = ["https://rockpal.onmicrosoft.com/${var.name}-api"]
  logo_image       = filebase64("images/icon-512.png")
  owners           = [ "495f7cc2-440d-4501-aabc-c1f8c51a3c3d" ]
  sign_in_audience = "AzureADandPersonalMicrosoftAccount"

  api {
    mapped_claims_enabled          = true
    requested_access_token_version = 2

    known_client_applications = []

    oauth2_permission_scope {
      admin_consent_description  = "Allows the app to access RockPal API as the signed-in user."
      admin_consent_display_name = "Access RockPal API"
      enabled                    = true
      id                         = random_uuid.api_permission_scope.result
      type                       = "Admin"
      user_consent_description   = null
      user_consent_display_name  = null
      value                      = "access_as_user"
    }
  }

  feature_tags {
    enterprise = false
    gallery    = false
  }

  web {
    # redirect_uris = ["https://app.example.net/account"]

    implicit_grant {
      access_token_issuance_enabled = false
      id_token_issuance_enabled     = false
    }
  }
}