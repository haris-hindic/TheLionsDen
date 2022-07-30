// GENERATED CODE - DO NOT MODIFY BY HAND

part of 'room_type_response.dart';

// **************************************************************************
// JsonSerializableGenerator
// **************************************************************************

RoomTypeResponse _$RoomTypeResponseFromJson(Map<String, dynamic> json) =>
    RoomTypeResponse()
      ..roomTypeId = json['roomTypeId'] as int?
      ..name = json['name'] as String?
      ..description = json['description'] as String?
      ..rules = json['rules'] as String?
      ..size = json['size'] as int?
      ..capacity = json['capacity'] as int?
      ..images = (json['images'] as List<dynamic>?)
          ?.map((e) => RoomImageResponse.fromJson(e as Map<String, dynamic>))
          .toList();

Map<String, dynamic> _$RoomTypeResponseToJson(RoomTypeResponse instance) =>
    <String, dynamic>{
      'roomTypeId': instance.roomTypeId,
      'name': instance.name,
      'description': instance.description,
      'rules': instance.rules,
      'size': instance.size,
      'capacity': instance.capacity,
      'images': instance.images,
    };
