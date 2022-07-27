// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'user_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

UserResponse _$UserResponseFromJson(Map<String, dynamic> json) => UserResponse()
  ..userId = json['userId'] as int?
  ..firstName = json['firstName'] as String?
  ..lastName = json['lastName'] as String?
  ..username = json['username'] as String?
  ..email = json['email'] as String?
  ..phoneNumber = json['phoneNumber'] as String?
  ..status = json['status'] as String?
  ..dateOfBirth = json['dateOfBirth'] == null
      ? null
      : DateTime.parse(json['dateOfBirth'] as String)
  ..gender = json['gender'] as String?
  ..roleId = json['roleId'] as int?
  ..fullName = json['fullName'] as String?
  ..roleName = json['roleName'] as String?;

Map<String, dynamic> _$UserResponseToJson(UserResponse instance) =>
    <String, dynamic>{
      'userId': instance.userId,
      'firstName': instance.firstName,
      'lastName': instance.lastName,
      'username': instance.username,
      'email': instance.email,
      'phoneNumber': instance.phoneNumber,
      'status': instance.status,
      'dateOfBirth': instance.dateOfBirth?.toIso8601String(),
      'gender': instance.gender,
      'roleId': instance.roleId,
      'fullName': instance.fullName,
      'roleName': instance.roleName,
    };
